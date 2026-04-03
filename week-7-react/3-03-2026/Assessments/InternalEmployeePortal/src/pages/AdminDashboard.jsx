// Importing employee context to access employee data
import { useEmployees } from "../context/EmployeeContext";

export default function AdminDashboard() {

  // Get all employees from global state
  const { employees } = useEmployees();

  return (
    <div className="page">

      {/* Page title */}
      <h1>Admin Dashboard</h1>

      {/* Stats section */}
      <div className="stats-grid">

        {/* Total employees card */}
        <div className="card">
          <h3>Total Employees</h3>
          <p>{employees.length}</p>
        </div>

        {/* Admin count */}
        <div className="card">
          <h3>Admins</h3>
          {/* Filtering employees with role = admin */}
          <p>{employees.filter((e) => e.role === "admin").length}</p>
        </div>

        {/* Employee count */}
        <div className="card">
          <h3>Employees</h3>
          {/* Filtering employees with role = employee */}
          <p>{employees.filter((e) => e.role === "employee").length}</p>
        </div>

      </div>
    </div>
  );
}