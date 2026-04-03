// Importing routing utilities from react-router-dom
import { Navigate, Route, Routes } from "react-router-dom";

// Importing reusable components and route guards
import Navbar from "./components/Navbar";
import ProtectedRoute from "./components/ProtectedRoute"; // Ensures user is logged in
import RoleProtectedRoute from "./components/RoleProtectedRoute"; // Ensures correct role access

// Importing authentication context to access logged-in user
import { useAuth } from "./context/AuthContext";

// Importing pages
import Login from "./pages/Login";
import AdminDashboard from "./pages/AdminDashboard";
import EmployeeDashboard from "./pages/EmployeeDashboard";
import ManageEmployees from "./pages/ManageEmployees";
import EmployeeProfile from "./pages/EmployeeProfile";
import NotFound from "./pages/NotFound";

export default function App() {
  // Getting current logged-in user from AuthContext
  const { user } = useAuth();

  return (
    <>
      {/* Navbar is always visible across all routes */}
      <Navbar />

      {/* Defining all application routes */}
      <Routes>

        {/* 
          Root route ("/")
          If user is NOT logged in → show Login page
          If user IS logged in → redirect to dashboard
        */}
        <Route 
          path="/" 
          element={!user ? <Login /> : <Navigate to="/dashboard" />} 
        />

        {/* 
          Dashboard route
          ProtectedRoute ensures only logged-in users can access
          Then role-based rendering:
            - Admin → AdminDashboard
            - Employee → EmployeeDashboard
        */}
        <Route
          path="/dashboard"
          element={
            <ProtectedRoute>
              {user?.role === "admin" 
                ? <AdminDashboard /> 
                : <EmployeeDashboard />}
            </ProtectedRoute>
          }
        />

        {/* 
          Manage Employees route (Admin only)
          
          Step 1: ProtectedRoute → user must be logged in
          Step 2: RoleProtectedRoute → only "admin" can access
          
          Prevents unauthorized users from accessing employee management
        */}
        <Route
          path="/manage-employees"
          element={
            <ProtectedRoute>
              <RoleProtectedRoute allowedRole="admin">
                <ManageEmployees />
              </RoleProtectedRoute>
            </ProtectedRoute>
          }
        />

        {/* 
          Profile route (accessible by all logged-in users)
          Shows individual employee profile
        */}
        <Route
          path="/profile"
          element={
            <ProtectedRoute>
              <EmployeeProfile />
            </ProtectedRoute>
          }
        />

        {/* 
          Fallback route
          If no route matches → show 404 page
        */}
        <Route path="*" element={<NotFound />} />

      </Routes>
    </>
  );
}