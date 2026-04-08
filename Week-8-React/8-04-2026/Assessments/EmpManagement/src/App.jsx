import { useState } from "react";
import { useSelector } from "react-redux";
import Header from "./components/Header";
import Login from "./components/Login";
import EmployeeForm from "./components/EmployeeForm";
import EmployeeList from "./components/EmployeeList";
import Loader from "./components/Loader";

export default function App() {
  const [editingEmployee, setEditingEmployee] = useState(null);

  const { isAuthenticated } = useSelector((state) => state.auth);
  const { theme, loading } = useSelector((state) => state.ui);

  return (
    <div className={`app ${theme}`}>
      {loading && <Loader />}

      <div className="container">
        {!isAuthenticated ? (
          <Login />
        ) : (
          <>
            <Header />
            <div className="dashboard-grid">
              <EmployeeForm
                editingEmployee={editingEmployee}
                setEditingEmployee={setEditingEmployee}
              />
              <EmployeeList setEditingEmployee={setEditingEmployee} />
            </div>
          </>
        )}
      </div>
    </div>
  );
}