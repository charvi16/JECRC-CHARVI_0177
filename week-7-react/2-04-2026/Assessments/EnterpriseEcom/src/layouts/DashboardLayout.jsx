import React from "react";
import { NavLink, Outlet } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

function DashboardLayout() {
  const { user } = useAuth();

  return (
    <div className="dashboard-layout">
      <aside className="dashboard-sidebar">
        <h2>Dashboard</h2>
        <p className="dashboard-user">Welcome, {user?.name}</p>

        <nav className="dashboard-nav">
          <NavLink to="/dashboard">Home</NavLink>
          <NavLink to="/dashboard/analytics">Analytics</NavLink>
          <NavLink to="/dashboard/settings">Settings</NavLink>
        </nav>
      </aside>

      <section className="dashboard-content">
        <Outlet />
      </section>
    </div>
  );
}

export default DashboardLayout;