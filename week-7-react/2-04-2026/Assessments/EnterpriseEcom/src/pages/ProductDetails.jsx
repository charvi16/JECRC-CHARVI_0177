import React from "react";
import { Link, NavLink, Outlet, useParams } from "react-router-dom";
import { useCart } from "../context/CartContext";
import { productsData } from "./Products";

function ProductDetails() {
  const { productId } = useParams();
  const { addToCart } = useCart();

  const product = productsData.find((item) => item.id === Number(productId));

  if (!product) {
    return (
      <div className="info-page">
        <h1>Product not found</h1>
        <Link to="/products">Back to Products</Link>
      </div>
    );
  }

  return (
    <div className="product-details-page">
      <div className="product-details-card">
        <img src={product.image} alt={product.name} className="details-image" />

        <div className="details-content">
          <h1>{product.name}</h1>
          <p className="product-category">{product.category}</p>
          <p className="product-rating">⭐ {product.rating}</p>
          <p className="product-price large-price">₹{product.price.toLocaleString()}</p>
          <p className="product-description">{product.description}</p>

          <button className="add-cart-btn big-btn" onClick={() => addToCart(product)}>
            Add to Cart
          </button>
        </div>
      </div>

      <div className="nested-nav">
        <NavLink to="reviews">Reviews</NavLink>
        <NavLink to="specs">Specs</NavLink>
      </div>

      <div className="nested-content">
        <Outlet context={{ product }} />
      </div>
    </div>
  );
}

export default ProductDetails;