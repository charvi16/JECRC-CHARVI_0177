import React, { useState } from "react";
import ProductList from "./components/ProductList";
import Cart from "./components/Cart";

function App() {
  const products = [
    { id: 1, name: "React T-Shirt", price: 25 },
    { id: 2, name: "JS Hoodie", price: 40 },
    { id: 3, name: "CSS Cap", price: 15 },
  ];

  const [cartItems, setCartItems] = useState([]);
  const [projectLink, setProjectLink] = useState("");

  const addToCart = (product) => {
    const existing = cartItems.find((item) => item.id === product.id);

    if (existing) {
      setCartItems(
        cartItems.map((item) =>
          item.id === product.id
            ? { ...item, quantity: item.quantity + 1 }
            : item
        )
      );
    } else {
      setCartItems([...cartItems, { ...product, quantity: 1 }]);
    }
  };

  const updateQuantity = (id, qty) => {
    if (qty <= 0) {
      setCartItems(cartItems.filter((item) => item.id !== id));
      return;
    }

    setCartItems(
      cartItems.map((item) =>
        item.id === id ? { ...item, quantity: qty } : item
      )
    );
  };

  const removeItem = (id) => {
    setCartItems(cartItems.filter((item) => item.id !== id));
  };

  return (
    <div style={styles.container}>
      <h1>Mini Shopping Cart</h1>

      <div style={styles.grid}>
        <ProductList products={products} onAddToCart={addToCart} />
        <Cart
          cartItems={cartItems}
          onUpdateQuantity={updateQuantity}
          onRemoveItem={removeItem}
        />
      </div>

    </div>
  );
}

const styles = {
  container: {
    padding: "20px",
    fontFamily: "Arial",
  },
  grid: {
    display: "grid",
    gridTemplateColumns: "1fr 1fr",
    gap: "20px",
  },
};

export default App;