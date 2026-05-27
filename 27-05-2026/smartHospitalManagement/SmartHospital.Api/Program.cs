using System.Collections.Concurrent;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});
builder.WebHost.UseKestrelCore();
builder.WebHost.UseUrls("http://127.0.0.1:5015");

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy => policy
        .WithOrigins("http://localhost:5173", "http://127.0.0.1:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});
builder.Services.AddSignalR();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddSingleton<HospitalStore>();
builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<AuditService>();
builder.Services.AddRoutingCore();

var app = builder.Build();

app.UseCors("frontend");
app.UseRouting();
app.UseMiddleware<JwtMiddleware>();

app.MapGet("/", () => Results.Ok(new
{
    service = "Smart Hospital Management API",
    modules = new[]
    {
        "Patient Portal", "Doctor Dashboard", "Appointment Scheduler",
        "Lab & Prescription Management", "Billing & Insurance",
        "Telemedicine", "Admin Analytics", "Emergency Tracking"
    }
}));

app.MapPost("/api/auth/login", (LoginRequest request, HospitalStore store, TokenService tokens, AuditService audit) =>
{
    var user = store.Users.Values.SingleOrDefault(u =>
        u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase));

    if (user is null || !PasswordHasher.Verify(request.Password, user.PasswordHash))
    {
        audit.Record("auth.failed", "anonymous", $"Failed login for {request.Email}");
        return Results.Unauthorized();
    }

    audit.Record("auth.login", user.Id, "User logged in");
    return Results.Ok(new AuthResponse(tokens.CreateToken(user), UserProfile.From(user)));
});

app.MapPost("/api/patients/register", (RegisterPatientRequest request, HospitalStore store, AuditService audit) =>
{
    var branch = store.Branches.Values.FirstOrDefault(b => b.Id == request.BranchId);
    if (branch is null)
    {
        return Results.BadRequest(new { message = "Invalid branch." });
    }

    var patient = new Patient(
        Id: Ids.New("pat"),
        FullName: request.FullName,
        Email: request.Email,
        Phone: request.Phone,
        BranchId: request.BranchId,
        DateOfBirth: request.DateOfBirth,
        InsuranceProvider: request.InsuranceProvider,
        RegisteredAt: DateTimeOffset.UtcNow);

    store.Patients[patient.Id] = patient;
    audit.Record("patient.registered", patient.Id, $"Patient registered at {branch.Name}");
    return Results.Created($"/api/patients/{patient.Id}", patient);
});

app.MapGet("/api/patients/me", (HttpContext context, HospitalStore store) =>
{
    var userId = context.RequireUserId();
    var patient = store.Patients.Values.FirstOrDefault(p => p.Email == store.Users[userId].Email);
    return patient is null ? Results.NotFound() : Results.Ok(patient);
}).RequireRoles(AppRoles.Patient, AppRoles.Admin);

app.MapGet("/api/doctors", (HospitalStore store) => Results.Ok(store.Doctors.Values));

app.MapGet("/api/doctors/dashboard", (HttpContext context, HospitalStore store) =>
{
    var doctorId = context.RequireUserId();
    var appointments = store.Appointments.Values
        .Where(a => a.DoctorId == doctorId)
        .OrderBy(a => a.StartsAt)
        .ToArray();

    return Results.Ok(new
    {
        today = appointments.Count(a => a.StartsAt.Date == DateTimeOffset.UtcNow.Date),
        waiting = appointments.Count(a => a.Status is AppointmentStatus.Booked or AppointmentStatus.CheckedIn),
        telemedicine = appointments.Count(a => a.Mode == AppointmentMode.Video),
        appointments
    });
}).RequireRoles(AppRoles.Doctor, AppRoles.Admin);

app.MapPost("/api/appointments", async (
    AppointmentRequest request,
    HospitalStore store,
    AuditService audit,
    IHubContext<HospitalHub> hub) =>
{
    if (!store.Patients.ContainsKey(request.PatientId) || !store.Doctors.ContainsKey(request.DoctorId))
    {
        return Results.BadRequest(new { message = "Invalid patient or doctor." });
    }

    var normalizedStart = request.StartsAt.ToUniversalTime();
    var normalizedEnd = normalizedStart.AddMinutes(request.DurationMinutes);
    Appointment appointment;

    lock (store.AppointmentLock)
    {
        var conflict = store.Appointments.Values.Any(a =>
            a.DoctorId == request.DoctorId &&
            a.Status != AppointmentStatus.Cancelled &&
            normalizedStart < a.EndsAt &&
            normalizedEnd > a.StartsAt);

        if (conflict)
        {
            audit.Record("appointment.conflict", request.PatientId,
                $"Doctor {request.DoctorId} already booked at {normalizedStart:u}");
            return Results.Conflict(new { message = "The selected doctor already has an appointment in this slot." });
        }

        appointment = new Appointment(
            Id: Ids.New("apt"),
            PatientId: request.PatientId,
            DoctorId: request.DoctorId,
            BranchId: request.BranchId,
            StartsAt: normalizedStart,
            EndsAt: normalizedEnd,
            Mode: request.Mode,
            Status: AppointmentStatus.Booked,
            Reason: request.Reason,
            VideoRoomUrl: request.Mode == AppointmentMode.Video
                ? $"https://teams.microsoft.com/l/meetup-join/{Guid.NewGuid():N}"
                : null);

        store.Appointments[appointment.Id] = appointment;
    }

    audit.Record("appointment.booked", request.PatientId, $"Appointment {appointment.Id} booked");
    await hub.Clients.Group($"doctor:{request.DoctorId}").SendAsync("appointmentBooked", appointment);
    await hub.Clients.Group($"branch:{request.BranchId}").SendAsync("appointmentBooked", appointment);
    return Results.Created($"/api/appointments/{appointment.Id}", appointment);
}).RequireRoles(AppRoles.Patient, AppRoles.Receptionist, AppRoles.Admin);

app.MapGet("/api/appointments", (HospitalStore store, string? branchId, string? doctorId) =>
{
    var appointments = store.Appointments.Values.AsEnumerable();
    if (!string.IsNullOrWhiteSpace(branchId))
    {
        appointments = appointments.Where(a => a.BranchId == branchId);
    }

    if (!string.IsNullOrWhiteSpace(doctorId))
    {
        appointments = appointments.Where(a => a.DoctorId == doctorId);
    }

    return Results.Ok(appointments.OrderBy(a => a.StartsAt));
}).RequireRoles(AppRoles.Doctor, AppRoles.Receptionist, AppRoles.Admin);

app.MapPost("/api/lab-reports", (LabReportRequest request, HospitalStore store, AuditService audit) =>
{
    var report = new LabReport(
        Id: Ids.New("lab"),
        PatientId: request.PatientId,
        DoctorId: request.DoctorId,
        TestName: request.TestName,
        ResultSummary: request.ResultSummary,
        Status: request.Status,
        CreatedAt: DateTimeOffset.UtcNow);

    store.LabReports[report.Id] = report;
    audit.Record("lab.report.created", request.PatientId, $"Lab report {report.Id} created");
    return Results.Created($"/api/lab-reports/{report.Id}", report);
}).RequireRoles(AppRoles.Doctor, AppRoles.LabTechnician, AppRoles.Admin);

app.MapPost("/api/prescriptions", (PrescriptionRequest request, HospitalStore store, AuditService audit) =>
{
    var prescription = new Prescription(
        Id: Ids.New("rx"),
        PatientId: request.PatientId,
        DoctorId: request.DoctorId,
        Medicines: request.Medicines,
        Notes: request.Notes,
        IssuedAt: DateTimeOffset.UtcNow);

    store.Prescriptions[prescription.Id] = prescription;
    audit.Record("prescription.created", request.PatientId, $"Prescription {prescription.Id} issued");
    return Results.Created($"/api/prescriptions/{prescription.Id}", prescription);
}).RequireRoles(AppRoles.Doctor, AppRoles.Admin);

app.MapGet("/api/pharmacy/inventory", (HospitalStore store, string? branchId) =>
{
    var inventory = store.Medicines.Values.AsEnumerable();
    if (!string.IsNullOrWhiteSpace(branchId))
    {
        inventory = inventory.Where(m => m.BranchId == branchId);
    }

    return Results.Ok(inventory.OrderBy(m => m.Name));
}).RequireRoles(AppRoles.Pharmacist, AppRoles.Doctor, AppRoles.Admin);

app.MapPost("/api/billing/invoices", (InvoiceRequest request, HospitalStore store, AuditService audit) =>
{
    var subtotal = request.Items.Sum(i => i.Quantity * i.UnitPrice);
    var insuranceCovered = Math.Round(subtotal * request.InsuranceCoveragePercent / 100m, 2);
    var payable = subtotal - insuranceCovered;

    var invoice = new Invoice(
        Id: Ids.New("inv"),
        PatientId: request.PatientId,
        Items: request.Items,
        Subtotal: subtotal,
        InsuranceCovered: insuranceCovered,
        Payable: payable,
        Status: InvoiceStatus.Pending,
        PaymentGatewayReference: null,
        CreatedAt: DateTimeOffset.UtcNow);

    store.Invoices[invoice.Id] = invoice;
    audit.Record("invoice.created", request.PatientId, $"Invoice {invoice.Id} payable {payable:C}");
    return Results.Created($"/api/billing/invoices/{invoice.Id}", invoice);
}).RequireRoles(AppRoles.Billing, AppRoles.Admin);

app.MapPost("/api/billing/invoices/{invoiceId}/pay", (string invoiceId, HospitalStore store, AuditService audit) =>
{
    if (!store.Invoices.TryGetValue(invoiceId, out var invoice))
    {
        return Results.NotFound();
    }

    var paid = invoice with
    {
        Status = InvoiceStatus.Paid,
        PaymentGatewayReference = $"PAY-{DateTimeOffset.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(1000, 9999)}"
    };
    store.Invoices[invoiceId] = paid;
    audit.Record("invoice.paid", invoice.PatientId, $"Invoice {invoiceId} paid");
    return Results.Ok(paid);
}).RequireRoles(AppRoles.Patient, AppRoles.Billing, AppRoles.Admin);

app.MapPost("/api/emergencies", async (
    EmergencyRequest request,
    HospitalStore store,
    AuditService audit,
    IHubContext<HospitalHub> hub) =>
{
    var incident = new EmergencyIncident(
        Id: Ids.New("emg"),
        BranchId: request.BranchId,
        PatientId: request.PatientId,
        Severity: request.Severity,
        Location: request.Location,
        Notes: request.Notes,
        Status: EmergencyStatus.Open,
        CreatedAt: DateTimeOffset.UtcNow);

    store.Emergencies[incident.Id] = incident;
    audit.Record("emergency.opened", request.PatientId ?? request.BranchId, $"Emergency {incident.Id} opened");
    await hub.Clients.Group($"branch:{request.BranchId}").SendAsync("emergencyOpened", incident);
    return Results.Created($"/api/emergencies/{incident.Id}", incident);
}).RequireRoles(AppRoles.Doctor, AppRoles.Receptionist, AppRoles.Admin);

app.MapPost("/api/ai/symptom-checker", (SymptomRequest request, AuditService audit) =>
{
    var symptoms = request.Symptoms.Select(s => s.ToLowerInvariant()).ToArray();
    var severity = symptoms.Any(s => s.Contains("chest pain") || s.Contains("breath") || s.Contains("stroke"))
        ? "Emergency"
        : symptoms.Length >= 3 ? "Priority" : "Routine";

    var recommendation = severity switch
    {
        "Emergency" => "Seek emergency care immediately and notify the nearest branch.",
        "Priority" => "Book a same-day appointment with a physician.",
        _ => "Book a routine consultation and monitor symptoms."
    };

    audit.Record("ai.symptom.check", request.PatientId ?? "anonymous", $"Severity: {severity}");
    return Results.Ok(new SymptomResponse(severity, recommendation, new[]
    {
        "This assistant is not a medical diagnosis.",
        "Escalate critical symptoms to emergency services."
    }));
});

app.MapGet("/api/admin/analytics", (HospitalStore store) => Results.Ok(new
{
    branches = store.Branches.Count,
    patients = store.Patients.Count,
    doctors = store.Doctors.Count,
    appointmentsToday = store.Appointments.Values.Count(a => a.StartsAt.Date == DateTimeOffset.UtcNow.Date),
    openEmergencies = store.Emergencies.Values.Count(e => e.Status == EmergencyStatus.Open),
    pendingInvoices = store.Invoices.Values.Count(i => i.Status == InvoiceStatus.Pending),
    revenue = store.Invoices.Values.Where(i => i.Status == InvoiceStatus.Paid).Sum(i => i.Payable),
    auditEvents = store.AuditEvents.Count
})).RequireRoles(AppRoles.Admin);

app.MapGet("/api/admin/audit", (HospitalStore store) =>
    Results.Ok(store.AuditEvents.OrderByDescending(e => e.Timestamp).Take(100))).RequireRoles(AppRoles.Admin);

app.MapHub<HospitalHub>("/hubs/hospital");

app.Run();

static class EndpointRoleExtensions
{
    public static RouteHandlerBuilder RequireRoles(this RouteHandlerBuilder builder, params string[] roles)
    {
        builder.AddEndpointFilter(async (context, next) =>
        {
            var http = context.HttpContext;
            if (http.User.Identity?.IsAuthenticated != true)
            {
                return Results.Unauthorized();
            }

            if (!roles.Any(http.User.IsInRole))
            {
                return Results.Forbid();
            }

            return await next(context);
        });
        return builder;
    }
}

static class HttpContextExtensions
{
    public static string RequireUserId(this HttpContext context) =>
        context.User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("Authenticated user id was missing.");
}

sealed class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context, TokenService tokens)
    {
        var bearer = context.Request.Headers.Authorization.FirstOrDefault();
        if (bearer?.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) == true)
        {
            var principal = tokens.ValidateToken(bearer["Bearer ".Length..]);
            if (principal is not null)
            {
                context.User = principal;
            }
        }

        await _next(context);
    }
}

sealed class HospitalHub : Hub
{
    public async Task JoinBranch(string branchId) => await Groups.AddToGroupAsync(Context.ConnectionId, $"branch:{branchId}");

    public async Task JoinDoctor(string doctorId) => await Groups.AddToGroupAsync(Context.ConnectionId, $"doctor:{doctorId}");
}

sealed class HospitalStore
{
    public object AppointmentLock { get; } = new();
    public ConcurrentDictionary<string, Branch> Branches { get; } = new();
    public ConcurrentDictionary<string, AppUser> Users { get; } = new();
    public ConcurrentDictionary<string, Patient> Patients { get; } = new();
    public ConcurrentDictionary<string, Doctor> Doctors { get; } = new();
    public ConcurrentDictionary<string, Appointment> Appointments { get; } = new();
    public ConcurrentDictionary<string, LabReport> LabReports { get; } = new();
    public ConcurrentDictionary<string, Prescription> Prescriptions { get; } = new();
    public ConcurrentDictionary<string, MedicineStock> Medicines { get; } = new();
    public ConcurrentDictionary<string, Invoice> Invoices { get; } = new();
    public ConcurrentDictionary<string, EmergencyIncident> Emergencies { get; } = new();
    public ConcurrentQueue<AuditEvent> AuditEvents { get; } = new();

    public HospitalStore()
    {
        var main = new Branch("br-main", "Central City Hospital", "Bengaluru", true);
        var north = new Branch("br-north", "North Branch Hospital", "Delhi", true);
        Branches[main.Id] = main;
        Branches[north.Id] = north;

        AddUser(new AppUser("admin-1", "admin@hospital.com", "Platform Admin", AppRoles.Admin, main.Id, PasswordHasher.Hash("Admin@123")));
        AddUser(new AppUser("doc-1", "doctor@hospital.com", "Dr. Asha Mehta", AppRoles.Doctor, main.Id, PasswordHasher.Hash("Doctor@123")));
        AddUser(new AppUser("pat-1", "patient@hospital.com", "Rohan Sharma", AppRoles.Patient, main.Id, PasswordHasher.Hash("Patient@123")));
        AddUser(new AppUser("bill-1", "billing@hospital.com", "Billing Officer", AppRoles.Billing, main.Id, PasswordHasher.Hash("Billing@123")));

        Doctors["doc-1"] = new Doctor("doc-1", "Dr. Asha Mehta", "Cardiology", main.Id, true, 18);
        Doctors["doc-2"] = new Doctor("doc-2", "Dr. Neeraj Rao", "General Medicine", north.Id, true, 12);

        Patients["pat-1"] = new Patient("pat-1", "Rohan Sharma", "patient@hospital.com", "+91-90000-10000", main.Id,
            new DateOnly(1991, 4, 8), "Contoso Health", DateTimeOffset.UtcNow.AddDays(-20));

        Medicines["med-1"] = new MedicineStock("med-1", main.Id, "Paracetamol 500mg", 420, 22.50m, true);
        Medicines["med-2"] = new MedicineStock("med-2", main.Id, "Amoxicillin 250mg", 80, 76.00m, true);
        Medicines["med-3"] = new MedicineStock("med-3", north.Id, "Insulin Pen", 28, 610.00m, true);
    }

    private void AddUser(AppUser user) => Users[user.Id] = user;
}

sealed class TokenService(IConfiguration configuration)
{
    private readonly byte[] _secret = Encoding.UTF8.GetBytes(
        configuration["Jwt:Secret"] ?? "dev-secret-change-in-azure-key-vault-with-32-chars");
    private readonly string _issuer = configuration["Jwt:Issuer"] ?? "SmartHospital.Api";

    public string CreateToken(AppUser user)
    {
        var header = Base64Url(JsonSerializer.SerializeToUtf8Bytes(new { alg = "HS256", typ = "JWT" }));
        var payload = Base64Url(JsonSerializer.SerializeToUtf8Bytes(new
        {
            sub = user.Id,
            email = user.Email,
            name = user.DisplayName,
            role = user.Role,
            branchId = user.BranchId,
            iss = _issuer,
            exp = DateTimeOffset.UtcNow.AddHours(8).ToUnixTimeSeconds()
        }));
        var signature = Sign($"{header}.{payload}");
        return $"{header}.{payload}.{signature}";
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var parts = token.Split('.');
        if (parts.Length != 3 || Sign($"{parts[0]}.{parts[1]}") != parts[2])
        {
            return null;
        }

        var payload = JsonSerializer.Deserialize<JsonElement>(Base64UrlDecode(parts[1]));
        if (!payload.TryGetProperty("exp", out var exp) ||
            DateTimeOffset.FromUnixTimeSeconds(exp.GetInt64()) < DateTimeOffset.UtcNow)
        {
            return null;
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, payload.GetProperty("sub").GetString()!),
            new Claim(ClaimTypes.Email, payload.GetProperty("email").GetString()!),
            new Claim(ClaimTypes.Name, payload.GetProperty("name").GetString()!),
            new Claim(ClaimTypes.Role, payload.GetProperty("role").GetString()!),
            new Claim("branchId", payload.GetProperty("branchId").GetString()!)
        };

        return new ClaimsPrincipal(new ClaimsIdentity(claims, "SmartHospitalJwt"));
    }

    private string Sign(string value)
    {
        using var hmac = new HMACSHA256(_secret);
        return Base64Url(hmac.ComputeHash(Encoding.UTF8.GetBytes(value)));
    }

    private static string Base64Url(byte[] bytes) =>
        Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');

    private static byte[] Base64UrlDecode(string value)
    {
        var incoming = value.Replace('-', '+').Replace('_', '/');
        incoming = incoming.PadRight(incoming.Length + (4 - incoming.Length % 4) % 4, '=');
        return Convert.FromBase64String(incoming);
    }
}

sealed class AuditService(HospitalStore store)
{
    public void Record(string action, string actorId, string details) =>
        store.AuditEvents.Enqueue(new AuditEvent(Ids.New("aud"), action, actorId, details, DateTimeOffset.UtcNow));
}

static class PasswordHasher
{
    public static string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100_000, HashAlgorithmName.SHA256, 32);
        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    public static bool Verify(string password, string stored)
    {
        var parts = stored.Split('.');
        if (parts.Length != 2)
        {
            return false;
        }

        var salt = Convert.FromBase64String(parts[0]);
        var expected = Convert.FromBase64String(parts[1]);
        var actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100_000, HashAlgorithmName.SHA256, 32);
        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}

static class Ids
{
    public static string New(string prefix) => $"{prefix}-{Guid.NewGuid():N}"[..16];
}

static class AppRoles
{
    public const string Admin = "Admin";
    public const string Doctor = "Doctor";
    public const string Patient = "Patient";
    public const string Receptionist = "Receptionist";
    public const string LabTechnician = "LabTechnician";
    public const string Pharmacist = "Pharmacist";
    public const string Billing = "Billing";
}

record AppUser(string Id, string Email, string DisplayName, string Role, string BranchId, string PasswordHash);
record Branch(string Id, string Name, string City, bool IsEmergencyEnabled);
record Patient(string Id, string FullName, string Email, string Phone, string BranchId, DateOnly DateOfBirth,
    string? InsuranceProvider, DateTimeOffset RegisteredAt);
record Doctor(string Id, string FullName, string Specialization, string BranchId, bool IsAvailable, int TodayCapacity);
record Appointment(string Id, string PatientId, string DoctorId, string BranchId, DateTimeOffset StartsAt,
    DateTimeOffset EndsAt, AppointmentMode Mode, AppointmentStatus Status, string Reason, string? VideoRoomUrl);
record LabReport(string Id, string PatientId, string DoctorId, string TestName, string ResultSummary,
    LabReportStatus Status, DateTimeOffset CreatedAt);
record Prescription(string Id, string PatientId, string DoctorId, IReadOnlyList<PrescriptionMedicine> Medicines,
    string Notes, DateTimeOffset IssuedAt);
record PrescriptionMedicine(string Name, string Dosage, string Frequency, int Days);
record MedicineStock(string Id, string BranchId, string Name, int Quantity, decimal UnitPrice, bool RequiresPrescription);
record Invoice(string Id, string PatientId, IReadOnlyList<InvoiceItem> Items, decimal Subtotal,
    decimal InsuranceCovered, decimal Payable, InvoiceStatus Status, string? PaymentGatewayReference,
    DateTimeOffset CreatedAt);
record InvoiceItem(string Description, int Quantity, decimal UnitPrice);
record EmergencyIncident(string Id, string BranchId, string? PatientId, EmergencySeverity Severity, string Location,
    string Notes, EmergencyStatus Status, DateTimeOffset CreatedAt);
record AuditEvent(string Id, string Action, string ActorId, string Details, DateTimeOffset Timestamp);

record LoginRequest(string Email, string Password);
record UserProfile(string Id, string Email, string DisplayName, string Role, string BranchId)
{
    public static UserProfile From(AppUser user) =>
        new(user.Id, user.Email, user.DisplayName, user.Role, user.BranchId);
}

record AuthResponse(string Token, UserProfile User);
record RegisterPatientRequest(string FullName, string Email, string Phone, string BranchId, DateOnly DateOfBirth,
    string? InsuranceProvider);
record AppointmentRequest(string PatientId, string DoctorId, string BranchId, DateTimeOffset StartsAt,
    int DurationMinutes, AppointmentMode Mode, string Reason);
record LabReportRequest(string PatientId, string DoctorId, string TestName, string ResultSummary, LabReportStatus Status);
record PrescriptionRequest(string PatientId, string DoctorId, IReadOnlyList<PrescriptionMedicine> Medicines, string Notes);
record InvoiceRequest(string PatientId, IReadOnlyList<InvoiceItem> Items, decimal InsuranceCoveragePercent);
record EmergencyRequest(string BranchId, string? PatientId, EmergencySeverity Severity, string Location, string Notes);
record SymptomRequest(string? PatientId, IReadOnlyList<string> Symptoms);
record SymptomResponse(string Severity, string Recommendation, IReadOnlyList<string> Disclaimers);

enum AppointmentMode { InPerson, Video }
enum AppointmentStatus { Booked, CheckedIn, Completed, Cancelled }
enum LabReportStatus { Ordered, InProgress, Completed, Reviewed }
enum InvoiceStatus { Pending, Paid, Failed, Refunded }
enum EmergencySeverity { Low, Medium, High, Critical }
enum EmergencyStatus { Open, Assigned, Stabilized, Closed }
