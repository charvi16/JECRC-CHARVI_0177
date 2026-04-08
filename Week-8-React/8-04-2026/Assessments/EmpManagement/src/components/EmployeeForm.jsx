import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import {
  addEmployee,
  updateEmployee,
} from "../redux/slices/employeeSlice";
import { setLoading } from "../redux/slices/uiSlice";

const initialForm = {
  id: null,
  name: "",
  email: "",
  department: "",
  designation: "",
};

export default function EmployeeForm({ editingEmployee, setEditingEmployee }) {
  const [formData, setFormData] = useState(initialForm);
  const dispatch = useDispatch();

  useEffect(() => {
    if (editingEmployee) {
      setFormData(editingEmployee);
    } else {
      setFormData(initialForm);
    }
  }, [editingEmployee]);

  const handleChange = (e) => {
    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (
      !formData.name.trim() ||
      !formData.email.trim() ||
      !formData.department.trim() ||
      !formData.designation.trim()
    ) {
      return;
    }

    dispatch(setLoading(true));

    setTimeout(() => {
      if (editingEmployee) {
        dispatch(updateEmployee(formData));
        setEditingEmployee(null);
      } else {
        dispatch(
          addEmployee({
            ...formData,
            id: Date.now(),
          })
        );
      }

      setFormData(initialForm);
      dispatch(setLoading(false));
    }, 500);
  };

  return (
    <form className="card form-card" onSubmit={handleSubmit}>
      <h2>{editingEmployee ? "Edit Employee" : "Add Employee"}</h2>

      <input
        type="text"
        name="name"
        placeholder="Full Name"
        value={formData.name}
        onChange={handleChange}
      />

      <input
        type="email"
        name="email"
        placeholder="Email"
        value={formData.email}
        onChange={handleChange}
      />

      <input
        type="text"
        name="department"
        placeholder="Department"
        value={formData.department}
        onChange={handleChange}
      />

      <input
        type="text"
        name="designation"
        placeholder="Designation"
        value={formData.designation}
        onChange={handleChange}
      />

      <div className="form-actions">
        <button type="submit">
          {editingEmployee ? "Update Employee" : "Add Employee"}
        </button>

        {editingEmployee && (
          <button
            type="button"
            className="secondary-btn"
            onClick={() => {
              setEditingEmployee(null);
              setFormData(initialForm);
            }}
          >
            Cancel
          </button>
        )}
      </div>
    </form>
  );
}