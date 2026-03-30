import React from "react";

function ProductList({ products, onAddToCart }) {
  return (
    <div style={styles.section}>
      <h2>Products</h2>

      {products.map((product) => (
        <div key={product.id} style={styles.card}>
          <div>
            <h3>{product.name}</h3>
            <p>${product.price}</p>
          </div>

          <button onClick={() => onAddToCart(product)} style={styles.button}>
            Add
          </button>
        </div>
      ))}
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
  button: {
    padding: "6px 12px",
    backgroundColor: "#007bff",
    color: "#fff",
    border: "none",
    borderRadius: "6px",
    cursor: "pointer",
  },
};

export default ProductList;