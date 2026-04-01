import React, { useEffect, useState } from "react";

function UseEffectRender() {
  const [count, setCount] = useState(0);
  const [text, setText] = useState("");

  useEffect(() => {
    console.log("Effect1: After every render");
  });

  return (
    <div style={styles.container}>
      <h1>useEffect - Every Render</h1>

      <h2>Count: {count}</h2>

      <button
        style={styles.btn}
        onClick={() => setCount(count + 1)}
      >
        Increment
      </button>

      <div style={{ marginTop: "20px" }}>
        <input
          type="text"
          placeholder="Type Something..."
          value={text}
          onChange={(e) => setText(e.target.value)}
          style={styles.input}
        />
      </div>

      <p style={styles.info}>
        This effect runs after <b>every render</b> (state change).
      </p>

      <p style={styles.note}>
        Open console to observe the logs
      </p>
    </div>
  );
}

const styles = {
  container: {
    textAlign: "center",
    marginTop: "50px",
    fontFamily: "Arial",
  },
  btn: {
    padding: "10px 20px",
    fontSize: "16px",
    marginTop: "10px",
    backgroundColor: "#007bff",
    color: "white",
    border: "none",
    borderRadius: "5px",
    cursor: "pointer",
  },
  input: {
    padding: "10px",
    width: "220px",
    borderRadius: "5px",
    border: "1px solid #ccc",
  },
  info: {
    marginTop: "20px",
    color: "blue",
  },
  note: {
    color: "gray",
  },
};

export default UseEffectRender;