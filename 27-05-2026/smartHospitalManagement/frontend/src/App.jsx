import React, { useEffect, useMemo, useState } from 'react';
import { createRoot } from 'react-dom/client';
import {
  Activity,
  AlertTriangle,
  BadgeIndianRupee,
  Bell,
  CalendarClock,
  ClipboardPlus,
  FlaskConical,
  LayoutDashboard,
  LogIn,
  Pill,
  ShieldCheck,
  Stethoscope,
  Video
} from 'lucide-react';
import * as signalR from '@microsoft/signalr';
import './styles.css';

const API = 'http://127.0.0.1:5015';

const demoUsers = [
  { label: 'Admin', email: 'admin@hospital.com', password: 'Admin@123' },
  { label: 'Doctor', email: 'doctor@hospital.com', password: 'Doctor@123' },
  { label: 'Patient', email: 'patient@hospital.com', password: 'Patient@123' },
  { label: 'Billing', email: 'billing@hospital.com', password: 'Billing@123' }
];

function upsertById(items, nextItem) {
  return uniqueById([nextItem, ...items]);
}

function uniqueById(items) {
  const seen = new Set();
  return items.filter((item) => {
    if (!item?.id || seen.has(item.id)) {
      return false;
    }

    seen.add(item.id);
    return true;
  });
}

function App() {
  const [session, setSession] = useState(null);
  const [analytics, setAnalytics] = useState(null);
  const [doctors, setDoctors] = useState([]);
  const [appointments, setAppointments] = useState([]);
  const [notifications, setNotifications] = useState([]);
  const [symptomResult, setSymptomResult] = useState(null);
  const [message, setMessage] = useState('');

  const authHeaders = useMemo(() => ({
    'Content-Type': 'application/json',
    Authorization: session ? `Bearer ${session.token}` : ''
  }), [session]);

  useEffect(() => {
    fetch(`${API}/api/doctors`)
      .then((response) => response.json())
      .then(setDoctors)
      .catch(() => setMessage('Start the API to load doctors.'));
  }, []);

  useEffect(() => {
    if (!session) return;

    const connection = new signalR.HubConnectionBuilder()
      .withUrl(`${API}/hubs/hospital`)
      .withAutomaticReconnect()
      .build();

    connection.on('appointmentBooked', (appointment) => {
      setNotifications((items) => [`New appointment: ${appointment.reason}`, ...items].slice(0, 5));
      setAppointments((items) => upsertById(items, appointment));
    });

    connection.on('emergencyOpened', (incident) => {
      setNotifications((items) => [`Emergency ${incident.severity}: ${incident.location}`, ...items].slice(0, 5));
    });

    connection.start()
      .then(async () => {
        await connection.invoke('JoinBranch', session.user.branchId);
        if (session.user.role === 'Doctor') {
          await connection.invoke('JoinDoctor', session.user.id);
        }
      })
      .catch(() => setMessage('Realtime connection is unavailable.'));

    return () => connection.stop();
  }, [session]);

  async function api(path, options = {}) {
    const response = await fetch(`${API}${path}`, {
      ...options,
      headers: { ...authHeaders, ...(options.headers || {}) }
    });

    if (!response.ok) {
      const body = await response.json().catch(() => ({}));
      throw new Error(body.message || `Request failed: ${response.status}`);
    }

    return response.json();
  }

  async function login(user) {
    setMessage('');
    try {
      const result = await api('/api/auth/login', {
        method: 'POST',
        body: JSON.stringify({ email: user.email, password: user.password }),
        headers: { 'Content-Type': 'application/json' }
      });
      setSession(result);
      setMessage(`Signed in as ${result.user.displayName}`);
      loadRoleData(result);
    } catch (error) {
      setMessage(error.message);
    }
  }

  async function loadRoleData(activeSession = session) {
    if (!activeSession) return;

    const headers = {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${activeSession.token}`
    };

    if (['Admin', 'Doctor', 'Receptionist'].includes(activeSession.user.role)) {
      fetch(`${API}/api/appointments?branchId=${activeSession.user.branchId}`, { headers })
        .then((response) => response.ok ? response.json() : [])
        .then((items) => setAppointments(uniqueById(items)));
    }

    if (activeSession.user.role === 'Admin') {
      fetch(`${API}/api/admin/analytics`, { headers })
        .then((response) => response.ok ? response.json() : null)
        .then(setAnalytics);
    }
  }

  async function bookAppointment(event) {
    event.preventDefault();
    const form = new FormData(event.currentTarget);
    const payload = {
      patientId: 'pat-1',
      doctorId: form.get('doctorId'),
      branchId: session.user.branchId,
      startsAt: new Date(form.get('startsAt')).toISOString(),
      durationMinutes: Number(form.get('durationMinutes')),
      mode: form.get('mode'),
      reason: form.get('reason')
    };

    try {
      const appointment = await api('/api/appointments', { method: 'POST', body: JSON.stringify(payload) });
      setAppointments((items) => upsertById(items, appointment));
      setMessage('Appointment booked. Notifications were sent to branch and doctor channels.');
      event.currentTarget.reset();
    } catch (error) {
      setMessage(error.message);
    }
  }

  async function runSymptomChecker(event) {
    event.preventDefault();
    const symptoms = new FormData(event.currentTarget).get('symptoms')
      .split(',')
      .map((value) => value.trim())
      .filter(Boolean);

    const result = await api('/api/ai/symptom-checker', {
      method: 'POST',
      body: JSON.stringify({ patientId: 'pat-1', symptoms })
    });
    setSymptomResult(result);
  }

  async function createEmergency() {
    const incident = await api('/api/emergencies', {
      method: 'POST',
      body: JSON.stringify({
        branchId: session.user.branchId,
        patientId: 'pat-1',
        severity: 'Critical',
        location: 'Ambulance bay',
        notes: 'Chest pain with shortness of breath'
      })
    });
    setMessage(`Emergency created: ${incident.id}`);
  }

  return (
    <main>
      <header className="topbar">
        <div>
          <span className="eyebrow">Centralized multi-branch platform</span>
          <h1>Smart Hospital Management</h1>
        </div>
        <div className="session">
          <ShieldCheck size={18} />
          {session ? `${session.user.displayName} · ${session.user.role}` : 'JWT + RBAC ready'}
        </div>
      </header>

      <section className="layout">
        <aside className="sidebar">
          <h2>Demo Access</h2>
          {demoUsers.map((user) => (
            <button key={user.email} className="login-button" onClick={() => login(user)}>
              <LogIn size={16} />
              {user.label}
            </button>
          ))}
          <div className="notice">
            <Bell size={16} />
            <div>
              <strong>Realtime</strong>
              {notifications.length === 0 ? <p>No live events yet.</p> : notifications.map((item, index) => <p key={`${item}-${index}`}>{item}</p>)}
            </div>
          </div>
        </aside>

        <section className="content">
          {message && <div className="banner">{message}</div>}

          <div className="metrics">
            <Metric icon={<LayoutDashboard />} label="Branches" value={analytics?.branches ?? 2} />
            <Metric icon={<Stethoscope />} label="Doctors" value={analytics?.doctors ?? doctors.length} />
            <Metric icon={<CalendarClock />} label="Appointments" value={analytics?.appointmentsToday ?? appointments.length} />
            <Metric icon={<AlertTriangle />} label="Open Emergencies" value={analytics?.openEmergencies ?? 0} />
          </div>

          <div className="workspace">
            <Panel title="Appointment Scheduler" icon={<CalendarClock />}>
              <form onSubmit={bookAppointment} className="form-grid">
                <label>
                  Doctor
                  <select name="doctorId" required>
                    {doctors.map((doctor) => (
                      <option key={doctor.id} value={doctor.id}>{doctor.fullName} · {doctor.specialization}</option>
                    ))}
                  </select>
                </label>
                <label>
                  Start
                  <input name="startsAt" type="datetime-local" required />
                </label>
                <label>
                  Duration
                  <select name="durationMinutes" defaultValue="30">
                    <option value="15">15 min</option>
                    <option value="30">30 min</option>
                    <option value="45">45 min</option>
                  </select>
                </label>
                <label>
                  Mode
                  <select name="mode" defaultValue="Video">
                    <option value="Video">Video</option>
                    <option value="InPerson">In person</option>
                  </select>
                </label>
                <label className="wide">
                  Reason
                  <input name="reason" placeholder="Fever, follow-up, cardiology review" required />
                </label>
                <button disabled={!session}><Video size={16} /> Book</button>
              </form>
            </Panel>

            <Panel title="Clinical Operations" icon={<ClipboardPlus />}>
              <div className="module-grid">
                <Module icon={<FlaskConical />} title="Lab Reports" text="Create, review, and expose patient reports by role." />
                <Module icon={<Pill />} title="Pharmacy" text="Branch inventory with prescription-only controls." />
                <Module icon={<BadgeIndianRupee />} title="Billing" text="Insurance coverage and payment reference tracking." />
                <Module icon={<Activity />} title="Audit Logs" text="Every sensitive workflow writes an auditable event." />
              </div>
            </Panel>

            <Panel title="AI Symptom Checker" icon={<Activity />}>
              <form onSubmit={runSymptomChecker} className="symptom-form">
                <input name="symptoms" placeholder="chest pain, shortness of breath, fever" required />
                <button disabled={!session}>Check</button>
              </form>
              {symptomResult && (
                <div className={`triage ${symptomResult.severity.toLowerCase()}`}>
                  <strong>{symptomResult.severity}</strong>
                  <p>{symptomResult.recommendation}</p>
                </div>
              )}
            </Panel>

            <Panel title="Emergency Tracking" icon={<AlertTriangle />}>
              <p className="muted">Create a critical emergency event to test SignalR branch notifications and high-priority operational tracking.</p>
              <button className="danger" disabled={!session} onClick={createEmergency}>
                <AlertTriangle size={16} /> Raise Emergency
              </button>
            </Panel>
          </div>

          <section className="table-section">
            <h2>Appointments</h2>
            <div className="table">
              <div className="row head">
                <span>Patient</span><span>Doctor</span><span>Starts</span><span>Mode</span><span>Status</span>
              </div>
              {appointments.map((appointment) => (
                <div className="row" key={appointment.id}>
                  <span>{appointment.patientId}</span>
                  <span>{appointment.doctorId}</span>
                  <span>{new Date(appointment.startsAt).toLocaleString()}</span>
                  <span>{appointment.mode}</span>
                  <span>{appointment.status}</span>
                </div>
              ))}
            </div>
          </section>
        </section>
      </section>
    </main>
  );
}

function Metric({ icon, label, value }) {
  return (
    <article className="metric">
      {React.cloneElement(icon, { size: 20 })}
      <span>{label}</span>
      <strong>{value}</strong>
    </article>
  );
}

function Panel({ title, icon, children }) {
  return (
    <section className="panel">
      <h2>{React.cloneElement(icon, { size: 19 })}{title}</h2>
      {children}
    </section>
  );
}

function Module({ icon, title, text }) {
  return (
    <article className="module">
      {React.cloneElement(icon, { size: 18 })}
      <strong>{title}</strong>
      <p>{text}</p>
    </article>
  );
}

createRoot(document.getElementById('root')).render(<App />);
