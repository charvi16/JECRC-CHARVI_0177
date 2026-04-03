// Importing auth context to access logged-in user
import { useAuth } from "../context/AuthContext";

export default function EmployeeDashboard() {

  // Get current user
  const { user } = useAuth();

  return (
    <div className="page">

      {/* Page title */}
      <h1>Employee Dashboard</h1>

      {/* Welcome card */}
      <div className="card">
        <h3>Welcome, {user.name}</h3>

        {/* Informational message about access restriction */}
        <p>You can only view your own profile and details.</p>
      </div>
    </div>
  );
}