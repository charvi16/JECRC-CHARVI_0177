import React from "react";
import { Link, NavLink, useNavigate } from "react-router-dom";
import { useCart } from "../context/CartContext";
import { useAuth } from "../context/AuthContext";

function Header() {
  const { totalItems } = useCart();
  const { user, logout, isAuthenticated } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  return (
    <header className="amazon-header">
      <div className="amazon-topbar">
        <Link to="/" className="brand-logo">
          <span className="brand-main">Enterprise</span>
          <span className="brand-sub">Ecom</span>
        </Link>

        <div className="search-box">
          <input type="text" placeholder="Search products, brands and more" />
          <button>Search</button>
        </div>

        <div className="header-actions">
          {isAuthenticated ? (
            <>
              <div className="header-user">
                <span>Hello, {user?.name}</span>
                <small>{user?.role}</small>
              </div>
              <NavLink to="/dashboard" className="header-link">
                Dashboard
              </NavLink>
              <button className="logout-btn" onClick={handleLogout}>
                Logout
              </button>
            </>
          ) : (
            <>
              <NavLink to="/login" className="header-link">
                Login
              </NavLink>
              <NavLink to="/register" className="header-link">
                Register
              </NavLink>
            </>
          )}

          <NavLink to="/cart" className="cart-btn">
            Cart <span className="cart-badge">{totalItems}</span>
          </NavLink>
        </div>
      </div>

      <nav className="amazon-nav">
        <NavLink to="/">Home</NavLink>
        <NavLink to="/about">About</NavLink>
        <NavLink to="/contact">Contact</NavLink>
        <NavLink to="/products">Products</NavLink>
      </nav>
    </header>
  );
}

export default Header;