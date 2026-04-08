import { useEmployees } from "../../context/employee/EmployeeContext";

export default function AnalyticsPanel() {
  const { totalEmployees, employees } = useEmployees();

  const engineeringCount = employees.filter(
    (emp) => emp.department === "Engineering"
  ).length;

  return (
    <div className="card">
      <h3>Analytics</h3>
      <p>Total Employees: {totalEmployees}</p>
      <p>Engineering Employees: {engineeringCount}</p>
    </div>
  );
}