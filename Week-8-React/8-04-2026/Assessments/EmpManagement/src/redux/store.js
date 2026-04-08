import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./slices/authSlice";
import employeeReducer from "./slices/employeeSlice";
import uiReducer from "./slices/uiSlice";
import logger from "./middleware/logger";
import { loadState, saveState } from "../utils/localStorage";

const persistedState = loadState();

const store = configureStore({
  reducer: {
    auth: authReducer,
    employees: employeeReducer,
    ui: uiReducer,
  },
  preloadedState: persistedState,
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(logger),
});

store.subscribe(() => {
  saveState({
    auth: store.getState().auth,
    employees: store.getState().employees,
    ui: store.getState().ui,
  });
});

export default store;