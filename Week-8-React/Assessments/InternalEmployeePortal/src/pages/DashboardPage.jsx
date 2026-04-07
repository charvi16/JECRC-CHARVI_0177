import { useAuth } from "../context/auth/AuthContext";
import EmployeeForm from "../components/employees/EmployeeForm";
import EmployeeList from "../components/employees/EmployeeList";
import AnalyticsPanel from '../components/dashboard/AnalyticsPanel';
import SettingsPanel from '../components/dashboard/SettingsPanel';
import Navbar from '../components/layout/Navbar';

export default function Dashboard() {
  const { user, logout } = useAuth();

  return (
    <div className="container">
      <Navbar/>
      <h2>Dashboard</h2>

      <p>Welcome: {user?.name}</p>
      <p>Role: {user?.role}</p>

      <button onClick={logout}>Logout</button>

      <div className="grid">
        <AnalyticsPanel />
        <SettingsPanel />
      </div>

      {/* 🔥 ONLY ADMIN CAN SEE FORM */}
      {user?.role === "Admin" && <EmployeeForm />}

      <EmployeeList />
    </div>
  );
}