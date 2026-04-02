import React, { createContext, useContext, useEffect, useState } from "react";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const savedUser = localStorage.getItem("enterpriseEcomUser");
    if (savedUser) {
      setUser(JSON.parse(savedUser));
    }
  }, []);

  const login = (email, password) => {
    if (!email || !password) {
      return { success: false, message: "Please fill all fields" };
    }

    const fakeUser = {
      name: email.split("@")[0],
      email,
      role: email.includes("admin") ? "admin" : "customer",
    };

    setUser(fakeUser);
    localStorage.setItem("enterpriseEcomUser", JSON.stringify(fakeUser));

    return { success: true };
  };

  const register = (name, email, password) => {
    if (!name || !email || !password) {
      return { success: false, message: "Please fill all fields" };
    }

    const fakeUser = {
      name,
      email,
      role: "customer",
    };

    setUser(fakeUser);
    localStorage.setItem("enterpriseEcomUser", JSON.stringify(fakeUser));

    return { success: true };
  };

  const logout = () => {
    setUser(null);
    localStorage.removeItem("enterpriseEcomUser");
  };

  return (
    <AuthContext.Provider value={{ user, login, register, logout, isAuthenticated: !!user }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);