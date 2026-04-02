import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

function Register() {
  const [form, setForm] = useState({ name: "", email: "", password: "" });
  const [error, setError] = useState("");
  const { register } = useAuth();
  const navigate = useNavigate();

  const handleChange = (e) => {
    setForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const result = register(form.name, form.email, form.password);

    if (!result.success) {
      setError(result.message);
      return;
    }

    navigate("/dashboard");
  };

  return (
    <div>
      <h2 className="auth-title">Create Account</h2>
      <form className="auth-form" onSubmit={handleSubmit}>
        <label>Name</label>
        <input
          type="text"
          name="name"
          placeholder="Enter full name"
          value={form.name}
          onChange={handleChange}
        />

        <label>Email</label>
        <input
          type="email"
          name="email"
          placeholder="Enter email"
          value={form.email}
          onChange={handleChange}
        />

        <label>Password</label>
        <input
          type="password"
          name="password"
          placeholder="Create password"
          value={form.password}
          onChange={handleChange}
        />

        {error && <p className="error-text">{error}</p>}

        <button type="submit">Register</button>
      </form>

      <p className="auth-switch">
        Already have an account? <Link to="/login">Sign in</Link>
      </p>
    </div>
  );
}

export default Register;