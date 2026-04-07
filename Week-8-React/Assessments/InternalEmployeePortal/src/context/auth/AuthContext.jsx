import { createContext, useContext, useReducer, useMemo, useCallback } from "react";

const AuthContext = createContext();

const initialState = {
  user: null,
  isAuthenticated: false,
};

function reducer(state, action) {
  switch (action.type) {
    case "LOGIN":
      return {
        user: action.payload,
        isAuthenticated: true,
      };

    case "LOGOUT":
      return initialState;

    default:
      return state;
  }
}

export function AuthProvider({ children }) {
  const [state, dispatch] = useReducer(reducer, initialState);

  // 🔥 HARDCODED USERS
  const login = useCallback((email, password) => {
    let user;

    if (email === "admin@company.com") {
      user = {
        id: 1,
        name: "Admin User",
        email,
        role: "Admin",
      };
    } else {
      user = {
        id: 2,
        name: "Normal Employee",
        email,
        role: "Employee",
      };
    }

    dispatch({ type: "LOGIN", payload: user });
  }, []);

  const logout = useCallback(() => {
    dispatch({ type: "LOGOUT" });
  }, []);

  const value = useMemo(() => {
    return {
      user: state.user,
      isAuthenticated: state.isAuthenticated,
      login,
      logout,
    };
  }, [state.user, state.isAuthenticated, login, logout]);

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  return useContext(AuthContext);
}