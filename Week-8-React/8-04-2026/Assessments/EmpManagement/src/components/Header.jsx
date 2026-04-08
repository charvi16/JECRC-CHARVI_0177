import { useDispatch, useSelector } from "react-redux";
import { logout } from "../redux/slices/authSlice";
import ThemeToggle from "./ThemeToggle";

export default function Header() {
  const dispatch = useDispatch();
  const { user } = useSelector((state) => state.auth);

  return (
    <header className="header">
      <div>
        <h1>Employee Management Dashboard</h1>
        <p>Welcome, {user?.username}</p>
      </div>

      <div className="header-actions">
        <ThemeToggle />
        <button className="danger-btn" onClick={() => dispatch(logout())}>
          Logout
        </button>
      </div>
    </header>
  );
}