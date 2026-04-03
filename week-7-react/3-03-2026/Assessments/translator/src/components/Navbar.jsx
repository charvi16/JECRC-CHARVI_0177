import { Link } from "react-router-dom";
import "./Navbar.css";

export default function Navbar() {
  return (
    <nav className="navbar">
      <div className="nav-left">
        <span className="logo">🌍 LingoLeap</span>
      </div>

      <div className="nav-links">
        <Link to="/">Home</Link>
        <Link to="/learn">Learn</Link>
        <Link to="/profile">Profile</Link>
      </div>
    </nav>
  );
}