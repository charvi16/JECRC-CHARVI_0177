import React from "react";
import { NavLink } from "react-router-dom";

function Sidebar() {
  return (
    <aside className="main-sidebar">
      <h3>Shop by Category</h3>
      <NavLink to="/products">All Products</NavLink>
      <NavLink to="/products">Electronics</NavLink>
      <NavLink to="/products">Fashion</NavLink>
      <NavLink to="/products">Home</NavLink>
      <NavLink to="/products">Beauty</NavLink>
      <NavLink to="/products">Deals</NavLink>
    </aside>
  );
}

export default Sidebar;