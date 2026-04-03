// React state
import { useState } from "react";

// Components
import EmployeeForm from "../components/EmployeeForm";
import Loader from "../components/Loader";

// Employee context
import { useEmployees } from "../context/EmployeeContext";

export default function ManageEmployees() {

  // Extract CRUD functions and state
  const { employees, addEmployee, updateEmployee, deleteEmployee, loading } = useEmployees();

  // Track employee being edited
  const [editingEmployee, setEditingEmployee] = useState(null);

  // Handle Add or Update
  const handleAddOrUpdate = async (employeeData) => {

    if (editingEmployee) {
      // Update existing employee
      await updateEmployee(employeeData);
      alert("Employee updated successfully");
      setEditingEmployee(null);
    } else {
      // Add new employee
      await addEmployee(employeeData);
      alert("Employee added successfully");
    }
  };

  // Handle delete
  const handleDelete = async (id) => {

    // Confirmation dialog
    const confirmed = window.confirm("Are you sure you want to delete this employee?");
    if (!confirmed) return;

    await deleteEmployee(id);
    alert("Employee deleted successfully");
  };

  return (
    <div className="page">

      <div className="manage-layout">

        {/* Form for add/edit */}
        <EmployeeForm
          onSubmit={handleAddOrUpdate}
          editingEmployee={editingEmployee}
          loading={loading}
        />

        {/* Employee list */}
        <div className="employee-list card">

          <h3>Employee Records</h3>

          {/* Show loader during operations */}
          {loading && <Loader />}

          {/* Empty state */}
          {!loading && employees.length === 0 && <p>No employees found.</p>}

          {/* Employee list rendering */}
          {!loading &&
            employees.map((emp) => (
              <div key={emp.id} className="employee-item">

                <div>
                  <h4>{emp.name}</h4>
                  <p>{emp.email}</p>
                  <p>{emp.employeeId}</p>
                  <p>{emp.department}</p>
                  <p>{emp.designation}</p>
                  <p className="role-tag">{emp.role}</p>
                </div>

                {/* Action buttons */}
                <div className="employee-actions">
                  <button onClick={() => setEditingEmployee(emp)}>Edit</button>

                  <button
                    className="danger"
                    onClick={() => handleDelete(emp.id)}
                  >
                    Delete
                  </button>
                </div>

              </div>
            ))}
        </div>
      </div>
    </div>
  );
}