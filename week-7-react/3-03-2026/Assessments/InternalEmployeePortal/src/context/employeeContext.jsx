import { createContext, useContext, useState } from "react";
import { initialEmployees } from "../data/mockData";

// Create Employee Context
// This will manage all employee-related data and operations (CRUD)
const EmployeeContext = createContext();

export function EmployeeProvider({ children }) {

  // Stores all employees (in-memory data)
  const [employees, setEmployees] = useState(initialEmployees);

  // Global loading state for CRUD operations
  const [loading, setLoading] = useState(false);

  // CREATE: Add new employee
  const addEmployee = (employee) => {
    return new Promise((resolve) => {
      setLoading(true);

      // Simulating API delay
      setTimeout(() => {

        // Generate unique ID using timestamp
        const newEmployee = {
          ...employee,
          id: Date.now(),
        };

        // Update state immutably
        setEmployees((prev) => [...prev, newEmployee]);

        setLoading(false);
        resolve(newEmployee);
      }, 700);
    });
  };

  // UPDATE: Edit existing employee
  const updateEmployee = (updatedEmployee) => {
    return new Promise((resolve) => {
      setLoading(true);

      setTimeout(() => {

        // Replace matching employee by ID
        setEmployees((prev) =>
          prev.map((emp) =>
            emp.id === updatedEmployee.id ? updatedEmployee : emp
          )
        );

        setLoading(false);
        resolve(updatedEmployee);
      }, 700);
    });
  };

  // DELETE: Remove employee
  const deleteEmployee = (id) => {
    return new Promise((resolve) => {
      setLoading(true);

      setTimeout(() => {

        // Filter out employee by ID
        setEmployees((prev) => prev.filter((emp) => emp.id !== id));

        setLoading(false);
        resolve();
      }, 700);
    });
  };

  // READ: Get employee by email (used for employee dashboard/profile)
  const getEmployeeByEmail = (email) => {
    return employees.find((emp) => emp.email === email);
  };

  return (
    // Providing employee data and CRUD operations globally
    <EmployeeContext.Provider
      value={{
        employees,
        loading,
        addEmployee,
        updateEmployee,
        deleteEmployee,
        getEmployeeByEmail,
      }}
    >
      {children}
    </EmployeeContext.Provider>
  );
}

// Custom hook for easy access to EmployeeContext
export function useEmployees() {
  return useContext(EmployeeContext);
}