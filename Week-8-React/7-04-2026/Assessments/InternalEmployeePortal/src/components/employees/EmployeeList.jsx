import { useEmployees } from "../../context/employee/EmployeeContext";
import { useAuth } from "../../context/auth/AuthContext";

export default function EmployeeList() {
  const { employees, deleteEmployee } = useEmployees();
  const { user } = useAuth();

  return (
    <div>
      <h3>Employee List</h3>

      {employees.map((emp) => (
        <div key={emp.id} style={{ border: "1px solid gray", margin: "10px", padding: "10px" }}>
          <p>{emp.name}</p>
          <p>{emp.email}</p>
          <p>{emp.department}</p>

          {/* 🔐 ONLY ADMIN CAN DELETE */}
          {user?.role === "Admin" && (
            <button onClick={() => deleteEmployee(emp.id)}>Delete</button>
          )}
        </div>
      ))}
    </div>
  );
}