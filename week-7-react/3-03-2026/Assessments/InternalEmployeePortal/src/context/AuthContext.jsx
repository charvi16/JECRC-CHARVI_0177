import { createContext, useContext, useEffect, useState } from "react";
import { mockUsers } from "../data/mockData";

// Create a global authentication context
// This will be used to share auth state (user, login, logout) across the app
const AuthContext = createContext();

export function AuthProvider({ children }) {

  // Stores currently logged-in user
  const [user, setUser] = useState(null);

  // Tracks loading state for authentication (login, initial load, etc.)
  const [authLoading, setAuthLoading] = useState(true);

  // Runs once when app loads
  // Purpose: Check if user is already logged in (persisted session)
  useEffect(() => {
    const storedUser = localStorage.getItem("portal_user");

    if (storedUser) {
      // Restore user session from localStorage
      setUser(JSON.parse(storedUser));
    }

    // Stop loading after checking storage
    setAuthLoading(false);
  }, []);

  // Login function (simulated API call using Promise)
  const login = (email, password) => {
    return new Promise((resolve, reject) => {
      setAuthLoading(true); // Start loader

      // Simulating backend API delay
      setTimeout(() => {

        // Check if user exists in mock data
        const foundUser = mockUsers.find(
          (u) => u.email === email && u.password === password
        );

        if (foundUser) {

          // Creating a safe user object (excluding sensitive data like password)
          const safeUser = {
            id: foundUser.id,
            email: foundUser.email,
            role: foundUser.role,
            name: foundUser.name,
            employeeId: foundUser.employeeId,
            department: foundUser.department,
            designation: foundUser.designation,
          };

          // Update state and persist user session
          setUser(safeUser);
          localStorage.setItem("portal_user", JSON.stringify(safeUser));

          setAuthLoading(false); // Stop loader
          resolve(safeUser); // Resolve promise
        } else {
          setAuthLoading(false);
          reject(new Error("Invalid email or password")); // Reject on failure
        }

      }, 1000);
    });
  };

  // Logout function
  const logout = () => {
    setUser(null); // Clear user from state
    localStorage.removeItem("portal_user"); // Remove session from storage
  };

  return (
    // Providing auth data and functions globally
    <AuthContext.Provider value={{ user, login, logout, authLoading }}>
      {children}
    </AuthContext.Provider>
  );
}

// Custom hook to use AuthContext easily in components
export function useAuth() {
  return useContext(AuthContext);
}