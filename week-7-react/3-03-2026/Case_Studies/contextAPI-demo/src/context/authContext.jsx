import React, { createContext, useState, useContext, useEffect } from "react";

const AuthContext = createContext();

export const useAuth = () => {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }

  return context;
};

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const storedUser = localStorage.getItem("user");

    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }

    setLoading(false);
  }, []);

  const login = async (username, password) => {
    setLoading(true);

    try {
      // simulate API call
      await new Promise((resolve) => setTimeout(resolve, 500));

      if (username === "admin" && password === "password") {
        const userData = { username: "admin" };

        setUser(userData);
        localStorage.setItem("user", JSON.stringify(userData));

        return userData; // ✅ optional but useful
      } else {
        throw new Error("Invalid Credentials");
      }
    } finally {
      setLoading(false);
    }
  };

  const logout = () => {
    setUser(null); // ✅ FIXED
    localStorage.removeItem("user");
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        loading,
        login,
        logout,
        isAuthenticated: !!user,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};