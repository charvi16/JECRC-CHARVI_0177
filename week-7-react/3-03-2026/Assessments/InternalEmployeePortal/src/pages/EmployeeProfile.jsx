// Importing contexts
import { useAuth } from "../context/AuthContext";
import { useEmployees } from "../context/EmployeeContext";

export default function EmployeeProfile() {

  // Get logged-in user
  const { user } = useAuth();

  // Access employee functions
  const { getEmployeeByEmail } = useEmployees();

  // Fetch employee data based on logged-in user's email
  const employee = getEmployeeByEmail(user.email);

  // Edge case: if no employee found
  if (!employee) {
    return (
      <div className="page">
        <div className="card">
          <h2>No employee data found</h2>
        </div>
      </div>
    );
  }

  return (
    <div className="page">

      {/* Page title */}
      <h1>My Profile</h1>

      {/* Employee details */}
      <div className="card profile-card">

        {/* Displaying employee information */}
        <p><strong>Name:</strong> {employee.name}</p>
        <p><strong>Email:</strong> {employee.email}</p>
        <p><strong>Employee ID:</strong> {employee.employeeId}</p>
        <p><strong>Department:</strong> {employee.department}</p>
        <p><strong>Designation:</strong> {employee.designation}</p>
        <p><strong>Role:</strong> {employee.role}</p>

      </div>
    </div>
  );
}