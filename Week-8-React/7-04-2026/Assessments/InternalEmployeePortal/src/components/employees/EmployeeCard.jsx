import React from "react";
import { useEmployees } from "../context/employee/EmployeeContext";

function EmployeeCard({ employee }) {
  const { deleteEmployee } = useEmployees();

  return (
    <div className="card">
      <h4>{employee.name}</h4>
      <p>{employee.email}</p>
      <p>{employee.department}</p>
      <button onClick={() => deleteEmployee(employee.id)}>Delete</button>
    </div>
  );
}

export default React.memo(EmployeeCard);