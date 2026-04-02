import React from "react";
import { Link } from "react-router-dom";

function Home() {
  return (
    <div>
      <section className="hero-banner">
        <div className="hero-overlay">
          <h1>Welcome to Enterprise E-Commerce</h1>
          <p>Shopping experience with React layouts and routing.</p>
          <Link to="/products" className="hero-btn">
            Shop Now
          </Link>
        </div>
      </section>

      <section className="home-grid">
        <div className="home-card">
          <h3>Electronics</h3>
          <p>Find trending gadgets, accessories, and premium devices.</p>
        </div>
        <div className="home-card">
          <h3>Fashion</h3>
          <p>Upgrade your wardrobe with modern styles and brands.</p>
        </div>
        <div className="home-card">
          <h3>Home Essentials</h3>
          <p>Everything for a smarter, cleaner, and better home.</p>
        </div>
        <div className="home-card">
          <h3>Top Deals</h3>
          <p>Grab limited-time offers and exciting product discounts.</p>
        </div>
      </section>
    </div>
  );
}

export default Home;