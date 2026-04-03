//This is mock data which is being used here becuase there is no backend in this project

export const mockUsers = [
  {
    id: 1,
    email: "admin@company.com",
    password: "admin123",
    role: "admin",
    name: "HR Admin",
    employeeId: "EMP001",
    department: "Human Resources",
    designation: "HR Manager",
  },
  {
    id: 2,
    email: "employee@company.com",
    password: "emp123",
    role: "employee",
    name: "John Employee",
    employeeId: "EMP002",
    department: "Engineering",
    designation: "Frontend Developer",
  },
];

export const initialEmployees = [
  {
    id: 1,
    name: "HR Admin",
    email: "admin@company.com",
    employeeId: "EMP001",
    department: "Human Resources",
    designation: "HR Manager",
    role: "admin",
  },
  {
    id: 2,
    name: "John Employee",
    email: "employee@company.com",
    employeeId: "EMP002",
    department: "Engineering",
    designation: "Frontend Developer",
    role: "employee",
  },
  {
    id: 3,
    name: "Sara Khan",
    email: "sara@company.com",
    employeeId: "EMP003",
    department: "Finance",
    designation: "Analyst",
    role: "employee",
  },
];