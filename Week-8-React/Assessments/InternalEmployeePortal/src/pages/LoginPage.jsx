import { useState } from "react";
import { useAuth } from "../context/auth/AuthContext";

export default function Login() {
  const { login } = useAuth();

  const [email, setEmail] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    login(email);
  };

  return (
    <div className="container">
      <h2>Login</h2>

      <form onSubmit={handleSubmit}>
        <input
          placeholder="Enter Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />

        <button>Login</button>
      </form>

      <p>👉 Use:</p>
      <p>admin@company.com → Admin</p>
      <p>anything else → Employee</p>
    </div>
  );
}