export const initialEmployeeState = {
  employees: [
    { id: 1, name: "Aarav Sharma", email: "aarav@company.com", department: "Engineering" },
    { id: 2, name: "Meera Gupta", email: "meera@company.com", department: "HR" },
  ],
};

export function employeeReducer(state, action) {
  switch (action.type) {
    case "ADD_EMPLOYEE":
      return {
        ...state,
        employees: [...state.employees, action.payload],
      };

    case "UPDATE_EMPLOYEE":
      return {
        ...state,
        employees: state.employees.map((emp) =>
          emp.id === action.payload.id ? action.payload : emp
        ),
      };

    case "DELETE_EMPLOYEE":
      return {
        ...state,
        employees: state.employees.filter((emp) => emp.id !== action.payload),
      };

    default:
      return state;
  }
}