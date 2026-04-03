import { Navigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

// RoleProtectedRoute component
// Purpose: Restrict access to routes based on user role (Authorization layer)
export default function RoleProtectedRoute({ children, allowedRole }) {

  // Get current logged-in user from AuthContext
  const { user } = useAuth();

  // If user is not logged in → redirect to login page
  // This acts as a fallback safety check (in case ProtectedRoute is not used)
  if (!user) return <Navigate to="/" replace />;

  // If user's role matches the allowed role → allow access
  // Otherwise → redirect to dashboard (safe fallback page)
  return user.role === allowedRole 
    ? children 
    : <Navigate to="/dashboard" replace />;
}