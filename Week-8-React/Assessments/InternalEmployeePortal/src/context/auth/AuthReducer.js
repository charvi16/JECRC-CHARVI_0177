export const initialAuthState = {
  user: null,
  isAuthenticated: false,
};

export function authReducer(state, action) {
  switch (action.type) {
    case "LOGIN":
      return {
        user: action.payload,
        isAuthenticated: true,
      };

    case "LOGOUT":
      return {
        user: null,
        isAuthenticated: false,
      };

    default:
      return state;
  }
}