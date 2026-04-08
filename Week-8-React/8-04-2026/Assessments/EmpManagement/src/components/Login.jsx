import { useState } from "react";
import { useDispatch } from "react-redux";
import { login } from "../redux/slices/authSlice";
import { setLoading } from "../redux/slices/uiSlice";

export default function Login() {
  const [username, setUsername] = useState("");
  const dispatch = useDispatch();

  const handleLogin = () => {
    if (!username.trim()) return;

    dispatch(setLoading(true));

    setTimeout(() => {
      dispatch(login({ username }));
      dispatch(setLoading(false));
    }, 800);
  };

  return (
    <div className="card login-card">
      <h2>Login</h2>
      <input
        type="text"
        placeholder="Enter username"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
      />
      <button onClick={handleLogin}>Login</button>
    </div>
  );
}