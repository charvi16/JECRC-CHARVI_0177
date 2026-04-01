import React, { useState, useEffect } from "react";

function App() {
  const [name, setName] = useState("");
  const [count, setCount] = useState(0);

  // Effect 1: Runs only once (Component Mount)
  useEffect(() => {
    console.log("Effect 2: Component mounted");

    // Load saved name
    const savedName = localStorage.getItem("name");
    if (savedName) setName(savedName);

    // Load saved count
    const savedCount = localStorage.getItem("count");
    if (savedCount) setCount(parseInt(savedCount));
  }, []);

  // Effect 2: Save data whenever it changes
  useEffect(() => {
    localStorage.setItem("name", name);
    localStorage.setItem("count", count);
  }, [name, count]);

  return (
    <div style={styles.container}>
      <h1>useEffect - Component Mount</h1>

      {/* Name Input */}
      <input
        type="text"
        placeholder="Enter your name"
        value={name}
        onChange={(e) => setName(e.target.value)}
        style={styles.input}
      />

      <h3>Hello, {name || "Guest"} 👋</h3>

      {/* Counter */}
      <h2>Count: {count}</h2>
      <button
        style={styles.btn}
        onClick={() => setCount(count + 1)}
      >
        Increment
      </button>
      <button style={styles.btn} onClick={() => {
        setName("");
        setCount(0);
        localStorage.clear();
      }}>
        Reset
      </button>

    </div>
  );
}

const styles = {
  container: {
    textAlign: "center",
    marginTop: "50px",
  },
  input: {
    padding: "10px",
    fontSize: "14px",
    marginTop: "20px",
  },
  btn: {
    padding: "10px 20px",
    fontSize: "16px",
    marginTop: "10px",
    cursor: "pointer",
  },
};

export default App;