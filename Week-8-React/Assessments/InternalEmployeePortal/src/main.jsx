import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import './index.css';

import { AuthProvider } from "./context/auth/AuthContext";
import { EmployeeProvider } from "./context/employee/EmployeeContext";
import {ThemeProvider} from './context/theme/ThemeContext';

ReactDOM.createRoot(document.getElementById("root")).render(
  <AuthProvider>
  <ThemeProvider>   {/* 🔥 YOU ARE MISSING THIS */}
    <EmployeeProvider>
      <App />
    </EmployeeProvider>
  </ThemeProvider>
</AuthProvider>
);