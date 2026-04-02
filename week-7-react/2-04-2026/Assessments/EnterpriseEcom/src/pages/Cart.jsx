import React from "react";
import { useCart } from "../context/CartContext";

function Cart() {
  const {
    cartItems,
    removeFromCart,
    increaseQty,
    decreaseQty,
    totalPrice,
    clearCart,
  } = useCart();

  return (
    <div>
      <div className="section-header">
        <h1>Your Cart</h1>
        <p>Review your selected items</p>
      </div>

      {cartItems.length === 0 ? (
        <div className="empty-cart">Your cart is empty.</div>
      ) : (
        <div className="cart-page">
          <div className="cart-list">
            {cartItems.map((item) => (
              <div className="cart-item" key={item.id}>
                <img src={item.image} alt={item.name} className="cart-item-img" />
                <div className="cart-item-info">
                  <h3>{item.name}</h3>
                  <p>{item.category}</p>
                  <p>₹{item.price.toLocaleString()}</p>

                  <div className="qty-box">
                    <button onClick={() => decreaseQty(item.id)}>-</button>
                    <span>{item.quantity}</span>
                    <button onClick={() => increaseQty(item.id)}>+</button>
                  </div>
                </div>

                <button className="remove-btn" onClick={() => removeFromCart(item.id)}>
                  Remove
                </button>
              </div>
            ))}
          </div>

          <div className="cart-summary">
            <h2>Order Summary</h2>
            <p>Total: ₹{totalPrice.toLocaleString()}</p>
            <button className="checkout-btn">Proceed to Checkout</button>
            <button className="clear-cart-btn" onClick={clearCart}>
              Clear Cart
            </button>
          </div>
        </div>
      )}
    </div>
  );
}

export default Cart;