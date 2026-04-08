import React, { useState } from "react";
import EmployeeList from "./features/employees/EmployeeList";
import EmployeeAdd from "./features/employees/EmployeeAdd";
import EmployeeEdit from "./features/employees/EmployeeEdit";

function App() {
  const [selectedEmployee, setSelectedEmployee] = useState(null);

  return (
    <div>
      <h1>Employee Management</h1>

      <EmployeeAdd />

      <EmployeeList
        onEdit={(employee) => setSelectedEmployee(employee)}
      />

      {selectedEmployee && (
        <EmployeeEdit
          selectedEmployee={selectedEmployee}
          clearEdit={() => setSelectedEmployee(null)}
        />
      )}
    </div>
  );
}

export default App;