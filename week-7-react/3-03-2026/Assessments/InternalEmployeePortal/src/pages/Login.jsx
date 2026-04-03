// React hooks
import { useState } from "react";

// Navigation hook
import { useNavigate } from "react-router-dom";

// Auth context
import { useAuth } from "../context/AuthContext";

// Loader component
import Loader from "../components/Loader";

export default function Login() {

  // Auth functions + loading state
  const { login, authLoading } = useAuth();

  const navigate = useNavigate();

  // Form state
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });

  // Error message state
  const [error, setError] = useState("");

  // Handle input changes
  const handleChange = (e) => {
    setError(""); // Clear error on typing

    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  // Basic validation
  const validate = () => {
    if (!formData.email.trim() || !formData.password.trim()) {
      setError("All fields are required");
      return false;
    }
    return true;
  };

  // Handle form submission
  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validate()) return;

    try {
      // Call login function
      const loggedInUser = await login(formData.email, formData.password);

      // Show success alert
      alert(`Welcome ${loggedInUser.name}`);

      // Role-based navigation
      if (loggedInUser.role === "admin") {
        navigate("/dashboard");
      } else {
        navigate("/profile");
      }

    } catch (err) {
      // Show error message
      setError(err.message);
    }
  };

  // Show loader while authentication is in progress
  if (authLoading) return <Loader />;

  return (
    <div className="login-container">

      <div className="login-card">

        {/* Title */}
        <h1>Internal Employee Portal</h1>
        <p>Login to continue</p>

        {/* Login form */}
        <form onSubmit={handleSubmit}>

          <input
            type="text"
            name="email"
            placeholder="Enter email"
            value={formData.email}
            onChange={handleChange}
          />

          <input
            type="password"
            name="password"
            placeholder="Enter password"
            value={formData.password}
            onChange={handleChange}
          />

          {/* Error message */}
          {error && <p className="error">{error}</p>}

          <button type="submit">Login</button>
        </form>

        {/* Demo credentials */}
        <div className="demo-box">
          <p><strong>Admin:</strong> admin@company.com / admin123</p>
          <p><strong>Employee:</strong> employee@company.com / emp123</p>
        </div>

      </div>
    </div>
  );
}