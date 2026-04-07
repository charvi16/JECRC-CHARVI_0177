import { createContext, useContext, useReducer, useMemo, useCallback } from "react";
import { useAuth } from "../auth/AuthContext";

const EmployeeContext = createContext();

const initialState = {
  employees: [
    { id: 1, name: "Aarav", email: "aarav@company.com", department: "Engineering" },
    { id: 2, name: "Meera", email: "meera@company.com", department: "HR" },
  ],
};

function reducer(state, action) {
  switch (action.type) {
    case "ADD":
      return {
        ...state,
        employees: [...state.employees, action.payload],
      };

    case "DELETE":
      return {
        ...state,
        employees: state.employees.filter((e) => e.id !== action.payload),
      };

    case "UPDATE":
      return {
        ...state,
        employees: state.employees.map((e) =>
          e.id === action.payload.id ? action.payload : e
        ),
      };

    default:
      return state;
  }
}

export function EmployeeProvider({ children }) {
  const [state, dispatch] = useReducer(reducer, initialState);
  const { user } = useAuth();

  // 🔐 ONLY ADMIN CAN ADD
  const addEmployee = useCallback(
    (emp) => {
      if (user?.role !== "Admin") {
        alert("Only Admin can add employees");
        return;
      }

      dispatch({
        type: "ADD",
        payload: { ...emp, id: Date.now() },
      });
    },
    [user]
  );

  // 🔐 ONLY ADMIN CAN DELETE
  const deleteEmployee = useCallback(
    (id) => {
      if (user?.role !== "Admin") {
        alert("Only Admin can delete employees");
        return;
      }

      dispatch({ type: "DELETE", payload: id });
    },
    [user]
  );

  const value = useMemo(() => {
    return {
      employees: state.employees,
      addEmployee,
      deleteEmployee,
    };
  }, [state.employees, addEmployee, deleteEmployee]);

  return <EmployeeContext.Provider value={value}>{children}</EmployeeContext.Provider>;
}

export function useEmployees() {
  return useContext(EmployeeContext);
}