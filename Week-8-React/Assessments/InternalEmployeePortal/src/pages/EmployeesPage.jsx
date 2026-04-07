import EmployeeForm from "../components/employees/EmployeeForm";
import EmployeeList from "../components/employees/EmployeeList";

export default function EmployeesPage() {
  return (
    <div className="employees-section">
      <h2>Employee Management</h2>
      <EmployeeForm />
      <EmployeeList />
    </div>
  );
}