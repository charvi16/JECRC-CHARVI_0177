import React from "react";
import ProductCard from "../components/ProductCard";

export const productsData = [
  {
    id: 1,
    name: "Wireless Headphones",
    category: "Electronics",
    price: 2999,
    rating: 4.4,
    image: "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=600",
    description: "High-quality wireless headphones with immersive sound.",
    specs: ["Bluetooth 5.3", "20h battery", "Noise isolation"],
    reviews: ["Amazing sound quality!", "Very comfortable.", "Worth the price."],
  },
  {
    id: 2,
    name: "Smart Watch",
    category: "Wearables",
    price: 4999,
    rating: 4.2,
    image: "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=600",
    description: "Track your fitness, notifications, and daily routines easily.",
    specs: ["Heart rate monitor", "Sleep tracking", "Water resistant"],
    reviews: ["Battery is good.", "Looks premium.", "Very useful for workouts."],
  },
  {
    id: 3,
    name: "Gaming Mouse",
    category: "Accessories",
    price: 1499,
    rating: 4.6,
    image: "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=600",
    description: "Ergonomic gaming mouse with RGB and precision control.",
    specs: ["RGB lighting", "7200 DPI", "Ergonomic grip"],
    reviews: ["Super smooth.", "Perfect for gaming.", "Clicks feel great."],
  },
  {
    id: 4,
    name: "Laptop Backpack",
    category: "Bags",
    price: 1999,
    rating: 4.3,
    image: "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=600",
    description: "Durable backpack with laptop sleeve and travel comfort.",
    specs: ["Water resistant", "15.6 inch laptop fit", "Multiple compartments"],
    reviews: ["Spacious and stylish.", "Good for office.", "Nice material."],
  },
];

function Products() {
  return (
    <div>
      <div className="section-header">
        <h1>Products</h1>
        <p>Explore our most popular products</p>
      </div>

      <div className="products-grid">
        {productsData.map((product) => (
          <ProductCard key={product.id} product={product} />
        ))}
      </div>
    </div>
  );
}

export default Products;