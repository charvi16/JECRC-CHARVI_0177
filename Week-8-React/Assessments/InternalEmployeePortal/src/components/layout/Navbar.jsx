import { useAuth } from "../../context/auth/AuthContext";
import useTheme from "../../hooks/useTheme";

export default function Navbar() {
  const { user, logout } = useAuth();
  const { theme, toggleTheme } = useTheme();

  return (
    <div className="navbar">
      <h2>Internal Employee Portal</h2>

      <div className="nav-actions">
        <span>{user?.name}</span>
        <button onClick={toggleTheme}>
          Switch to {theme === "light" ? "Dark" : "Light"} Theme
        </button>
        <button onClick={logout}>Logout</button>
      </div>
    </div>
  );
}