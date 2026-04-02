import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

function Login() {
  const [form, setForm] = useState({ email: "", password: "" });
  const [error, setError] = useState("");
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleChange = (e) => {
    setForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const result = login(form.email, form.password);

    if (!result.success) {
      setError(result.message);
      return;
    }

    navigate("/dashboard");
  };

  return (
    <div>
      <h2 className="auth-title">Sign In</h2>
      <form className="auth-form" onSubmit={handleSubmit}>
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
          placeholder="Enter password"
          value={form.password}
          onChange={handleChange}
        />

        {error && <p className="error-text">{error}</p>}

        <button type="submit">Login</button>
      </form>

      <p className="auth-switch">
        New here? <Link to="/register">Create an account</Link>
      </p>
    </div>
  );
}

export default Login;