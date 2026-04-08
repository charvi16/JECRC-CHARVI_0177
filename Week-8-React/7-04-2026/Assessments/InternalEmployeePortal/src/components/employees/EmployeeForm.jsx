import { useState } from "react";
import { useEmployees } from "../../context/employee/EmployeeContext";

export default function EmployeeForm() {
  const { addEmployee } = useEmployees();

  const [form, setForm] = useState({
    name: "",
    email: "",
    department: "",
  });

  const handleSubmit = (e) => {
    e.preventDefault();
    addEmployee(form);

    setForm({ name: "", email: "", department: "" });
  };

  return (
    <form onSubmit={handleSubmit}>
      <h3>Add Employee</h3>

      <input
        placeholder="Name"
        value={form.name}
        onChange={(e) => setForm({ ...form, name: e.target.value })}
      />

      <input
        placeholder="Email"
        value={form.email}
        onChange={(e) => setForm({ ...form, email: e.target.value })}
      />

      <input
        placeholder="Department"
        value={form.department}
        onChange={(e) => setForm({ ...form, department: e.target.value })}
      />

      <button>Add</button>
    </form>
  );
}