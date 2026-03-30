import React from "react";

function Cart({ cartItems, onUpdateQuantity, onRemoveItem }) {
  const total = cartItems.reduce(
    (sum, item) => sum + item.price * item.quantity,
    0
  );

  return (
    <div style={styles.section}>
      <h2>Cart</h2>

      {cartItems.length === 0 ? (
        <p>Cart is empty</p>
      ) : (
        <>
          {cartItems.map((item) => (
            <div key={item.id} style={styles.card}>
              <div>
                <h3>{item.name}</h3>
                <p>
                  ${item.price} x {item.quantity} = $
                  {item.price * item.quantity}
                </p>
              </div>

              <div>
                <button onClick={() => onUpdateQuantity(item.id, item.quantity - 1)}>
                  -
                </button>

                <span style={{ margin: "0 10px" }}>{item.quantity}</span>

                <button onClick={() => onUpdateQuantity(item.id, item.quantity + 1)}>
                  +
                </button>

                <button
                  onClick={() => onRemoveItem(item.id)}
                  style={{ marginLeft: "10px", color: "red" }}
                >
                  Remove
                </button>
              </div>
            </div>
          ))}

          <h3>Total: ${total}</h3>
        </>
      )}
    </div>
  );
}

const styles = {
  section: {
    padding: "20px",
    border: "1px solid #ddd",
    borderRadius: "10px",
  },
  card: {
    display: "flex",
    justifyContent: "space-between",
    marginBottom: "10px",
  },
};

export default Cart;