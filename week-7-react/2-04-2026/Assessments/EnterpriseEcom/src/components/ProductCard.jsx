import React from "react";
import { Link } from "react-router-dom";
import { useCart } from "../context/CartContext";

function ProductCard({ product }) {
  const { addToCart } = useCart();

  return (
    <div className="product-card">
      <img src={product.image} alt={product.name} className="product-img" />
      <div className="product-info">
        <h3>{product.name}</h3>
        <p className="product-category">{product.category}</p>
        <p className="product-rating">⭐ {product.rating}</p>
        <p className="product-price">₹{product.price.toLocaleString()}</p>

        <div className="product-card-actions">
          <Link to={`/products/${product.id}`} className="details-btn">
            View Details
          </Link>
          <button onClick={() => addToCart(product)} className="add-cart-btn">
            Add to Cart
          </button>
        </div>
      </div>
    </div>
  );
}

export default ProductCard;