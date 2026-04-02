import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";

import MainLayout from "./layouts/MainLayout";
import AuthLayout from "./layouts/AuthLayout";
import DashboardLayout from "./layouts/DashboardLayout";

import ProtectedRoute from "./components/ProtectedRoute";

import Home from "./pages/Home";
import About from "./pages/About";
import Contact from "./pages/Contact";
import Login from "./pages/Login";
import Register from "./pages/Register";
import DashboardHome from "./pages/DashboardHome";
import Analytics from "./pages/Analytics";
import Settings from "./pages/Settings";
import Products from "./pages/Products";
import ProductDetails from "./pages/ProductDetails";
import ProductReviews from "./pages/ProductReviews";
import ProductSpecs from "./pages/ProductSpecs";
import Cart from "./pages/Cart";
import NotFound from "./pages/NotFound";

function App() {
  return (
    <Routes>
      {/* Public Website */}
      <Route path="/" element={<MainLayout />}>
        <Route index element={<Home />} />
        <Route path="about" element={<About />} />
        <Route path="contact" element={<Contact />} />
        <Route path="products" element={<Products />} />
        <Route path="cart" element={<Cart />} />

        {/* Dynamic + Nested Routing */}
        <Route path="products/:productId" element={<ProductDetails />}>
          <Route index element={<Navigate to="reviews" replace />} />
          <Route path="reviews" element={<ProductReviews />} />
          <Route path="specs" element={<ProductSpecs />} />
        </Route>
      </Route>

      {/* Auth Module */}
      <Route path="/" element={<AuthLayout />}>
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
      </Route>

      {/* Dashboard Module */}
      <Route
        path="/dashboard"
        element={
          <ProtectedRoute>
            <DashboardLayout />
          </ProtectedRoute>
        }
      >
        <Route index element={<DashboardHome />} />
        <Route path="analytics" element={<Analytics />} />
        <Route path="settings" element={<Settings />} />
      </Route>

      <Route path="*" element={<NotFound />} />
    </Routes>
  );
}

export default App;