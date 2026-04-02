import React from "react";

function Analytics() {
  return (
    <div>
      <h1>Analytics</h1>
      <div className="dashboard-cards">
        <div className="dashboard-card">
          <h3>Weekly Sales</h3>
          <p>₹58,000</p>
        </div>
        <div className="dashboard-card">
          <h3>Conversion Rate</h3>
          <p>4.8%</p>
        </div>
        <div className="dashboard-card">
          <h3>New Customers</h3>
          <p>39</p>
        </div>
      </div>
    </div>
  );
}

export default Analytics;