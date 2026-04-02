import React from "react";
import { Outlet, Link } from "react-router-dom";

function AuthLayout() {
  return (
    <div className="auth-layout">
      <div className="auth-box">
        <Link to="/" className="auth-brand">
          Enterprise<span>Ecom</span>
        </Link>
        <Outlet />
      </div>
    </div>
  );
}

export default AuthLayout;