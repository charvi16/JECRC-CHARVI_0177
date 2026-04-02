import React from "react";

function DashboardHome() {
  return (
    <div>
      <h1>Dashboard Home</h1>
      <div className="dashboard-cards">
        <div className="dashboard-card">
          <h3>Total Orders</h3>
          <p>124</p>
        </div>
        <div className="dashboard-card">
          <h3>Total Revenue</h3>
          <p>₹2,45,000</p>
        </div>
        <div className="dashboard-card">
          <h3>Pending Deliveries</h3>
          <p>18</p>
        </div>
      </div>
    </div>
  );
}

export default DashboardHome;