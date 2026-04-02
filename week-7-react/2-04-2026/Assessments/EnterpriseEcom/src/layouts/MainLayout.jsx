import React from "react";
import { Outlet } from "react-router-dom";
import Header from "../components/Header";
import Sidebar from "../components/Sidebar";
import Footer from "../components/Footer";

function MainLayout() {
  return (
    <div className="app-shell">
      <Header />
      <div className="layout-body">
        <Sidebar />
        <main className="page-content">
          <Outlet />
        </main>
      </div>
      <Footer />
    </div>
  );
}

export default MainLayout;