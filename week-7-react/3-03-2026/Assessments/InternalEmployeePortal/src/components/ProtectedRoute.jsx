import { Navigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import Loader from "./Loader";

export default function ProtectedRoute({ children }) {

  // Get authentication state
  const { user, authLoading } = useAuth();

  // While checking authentication → show loader
  // Prevents flicker (important for UX)
  if (authLoading) return <Loader />;

  // If user is logged in → allow access to route
  // If not → redirect to login page
  return user ? children : <Navigate to="/" replace />;
}