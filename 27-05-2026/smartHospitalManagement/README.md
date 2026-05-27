# Smart Hospital Management

Centralized multi-branch hospital management starter using ASP.NET Core Web API, React, SQL Server deployment configuration, JWT-style authentication, RBAC, SignalR notifications, Docker, and Azure DevOps.

## What Is Included

- Patient registration and patient portal endpoint.
- Doctor dashboard and appointment list.
- Appointment booking with doctor/time-slot conflict detection.
- SignalR events for appointment and emergency updates.
- Lab report and prescription workflows.
- Pharmacy inventory endpoint.
- Billing, insurance coverage, and payment reference workflow.
- Emergency incident creation and branch-level realtime broadcast.
- AI symptom checker stub with deterministic triage rules.
- Admin analytics and audit event feed.
- React dashboard for login, scheduling, triage, notifications, and emergency testing.
- Docker Compose with SQL Server and API containers.
- Azure DevOps pipeline skeleton.

## Run Locally

Start the API:

```bash
dotnet run --project SmartHospital.Api
```

Start the frontend:

```bash
cd frontend
npm install
npm run dev
```

Open `http://127.0.0.1:5173`.

## Demo Users

| Role | Email | Password |
| --- | --- | --- |
| Admin | `admin@hospital.com` | `Admin@123` |
| Doctor | `doctor@hospital.com` | `Doctor@123` |
| Patient | `patient@hospital.com` | `Patient@123` |
| Billing | `billing@hospital.com` | `Billing@123` |

## Production Architecture

Use SQL Server for durable data, Azure Key Vault for secrets, Azure SignalR Service for realtime scale-out, Azure App Service or Azure Container Apps for API hosting, Azure Front Door and Application Gateway for high availability, and Application Insights for tracing.

Recommended service split:

- `SmartHospital.Api`: patient, appointment, doctor, lab, emergency, analytics APIs.
- `Billing.Service`: invoice, insurance, payment gateway callbacks, reconciliation.
- `Pharmacy.Service`: branch inventory, dispensing, stock movement, supplier integration.
- Shared event bus: Azure Service Bus topics for appointment booked, prescription issued, invoice paid, emergency opened.

## Security Notes

The current implementation is dependency-light for local training and uses in-memory storage plus a custom HMAC JWT middleware. For production, replace it with ASP.NET Core Identity, Entity Framework Core, SQL Server persistence, refresh tokens, row-level branch scoping, encryption at rest, stricter PHI audit trails, and managed identities for Azure resources.

## Key API Routes

- `POST /api/auth/login`
- `POST /api/patients/register`
- `GET /api/doctors`
- `POST /api/appointments`
- `GET /api/appointments`
- `POST /api/lab-reports`
- `POST /api/prescriptions`
- `GET /api/pharmacy/inventory`
- `POST /api/billing/invoices`
- `POST /api/billing/invoices/{invoiceId}/pay`
- `POST /api/emergencies`
- `POST /api/ai/symptom-checker`
- `GET /api/admin/analytics`
- `GET /api/admin/audit`
- SignalR hub: `/hubs/hospital`
