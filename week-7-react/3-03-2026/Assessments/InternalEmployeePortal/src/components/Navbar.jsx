import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

export default function Navbar() {

  // Access user info and logout function from AuthContext
  const { user, logout } = useAuth();

  const navigate = useNavigate();

  // Handles logout functionality
  const handleLogout = () => {
    logout(); // Clear user session
    alert("Logged out successfully"); // UI feedback
    navigate("/"); // Redirect to login page
  };

  // If user is not logged in → don't show navbar
  if (!user) return null;

  return (
    <nav className="navbar">

      {/* App title/logo */}
      <div>
        <h2>Employee Portal</h2>
      </div>

      <div className="nav-links">

        {/* Accessible to all logged-in users */}
        <Link to="/dashboard">Dashboard</Link>

        {/* Only visible to admin users */}
        {user.role === "admin" && (
          <Link to="/manage-employees">Manage Employees</Link>
        )}

        {/* Only visible to employee users */}
        {user.role === "employee" && (
          <Link to="/profile">My Profile</Link>
        )}

        {/* Display logged-in user info */}
        <span className="user-badge">
          {user.name} ({user.role})
        </span>

        {/* Logout button */}
        <button className="logout-btn" onClick={handleLogout}>
          Logout
        </button>
      </div>
    </nav>
  );
}