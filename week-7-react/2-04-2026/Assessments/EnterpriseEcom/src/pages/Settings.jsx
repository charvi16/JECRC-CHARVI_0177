import React from "react";
import { useAuth } from "../context/AuthContext";

function Settings() {
  const { user } = useAuth();

  return (
    <div>
      <h1>Settings</h1>
      <div className="settings-box">
        <p><strong>Name:</strong> {user?.name}</p>
        <p><strong>Email:</strong> {user?.email}</p>
        <p><strong>Role:</strong> {user?.role}</p>
      </div>
    </div>
  );
}

export default Settings;