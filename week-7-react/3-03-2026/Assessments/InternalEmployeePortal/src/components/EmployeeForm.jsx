import { useEffect, useState } from "react";

// Initial form state (used for resetting form and default values)
const initialFormState = {
  name: "",
  email: "",
  employeeId: "",
  department: "",
  designation: "",
  role: "employee",
};

export default function EmployeeForm({ onSubmit, editingEmployee, loading }) {

  // State to store form input values
  const [formData, setFormData] = useState(initialFormState);

  // State to store validation errors
  const [errors, setErrors] = useState({});

  // Runs whenever editingEmployee changes
  // If editing → pre-fill form with employee data
  // If adding → reset to initial state
  useEffect(() => {
    if (editingEmployee) {
      setFormData(editingEmployee);
    } else {
      setFormData(initialFormState);
    }
  }, [editingEmployee]);

  // Validation function for form fields
  const validate = () => {
    const newErrors = {};

    // Basic required field checks
    if (!formData.name.trim()) newErrors.name = "Name is required";

    if (!formData.email.trim()) {
      newErrors.email = "Email is required";
    } 
    // Email format validation using regex
    else if (!/\S+@\S+\.\S+/.test(formData.email)) {
      newErrors.email = "Invalid email format";
    }

    if (!formData.employeeId.trim()) newErrors.employeeId = "Employee ID is required";
    if (!formData.department.trim()) newErrors.department = "Department is required";
    if (!formData.designation.trim()) newErrors.designation = "Designation is required";

    // Update error state
    setErrors(newErrors);

    // Return true if no errors
    return Object.keys(newErrors).length === 0;
  };

  // Handles input changes dynamically
  const handleChange = (e) => {
    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  // Handles form submission
  const handleSubmit = (e) => {
    e.preventDefault();

    // Validate before submitting
    if (!validate()) return;

    // Call parent handler (add or update employee)
    onSubmit(formData);

    // Reset form only when adding (not editing)
    if (!editingEmployee) {
      setFormData(initialFormState);
    }
  };

  return (
    <form className="employee-form" onSubmit={handleSubmit}>

      {/* Dynamic heading based on mode */}
      <h3>{editingEmployee ? "Edit Employee" : "Add Employee"}</h3>

      {/* Name input */}
      <input
        type="text"
        name="name"
        placeholder="Full Name"
        value={formData.name}
        onChange={handleChange}
      />
      {errors.name && <small className="error">{errors.name}</small>}

      {/* Email input */}
      <input
        type="text"
        name="email"
        placeholder="Email"
        value={formData.email}
        onChange={handleChange}
      />
      {errors.email && <small className="error">{errors.email}</small>}

      {/* Employee ID input */}
      <input
        type="text"
        name="employeeId"
        placeholder="Employee ID"
        value={formData.employeeId}
        onChange={handleChange}
      />
      {errors.employeeId && <small className="error">{errors.employeeId}</small>}

      {/* Department input */}
      <input
        type="text"
        name="department"
        placeholder="Department"
        value={formData.department}
        onChange={handleChange}
      />
      {errors.department && <small className="error">{errors.department}</small>}

      {/* Designation input */}
      <input
        type="text"
        name="designation"
        placeholder="Designation"
        value={formData.designation}
        onChange={handleChange}
      />
      {errors.designation && <small className="error">{errors.designation}</small>}

      {/* Role dropdown */}
      <select name="role" value={formData.role} onChange={handleChange}>
        <option value="employee">Employee</option>
        <option value="admin">Admin</option>
      </select>

      {/* Submit button with loading state */}
      <button type="submit" disabled={loading}>
        {loading 
          ? "Processing..." 
          : editingEmployee 
            ? "Update Employee" 
            : "Add Employee"}
      </button>
    </form>
  );
}