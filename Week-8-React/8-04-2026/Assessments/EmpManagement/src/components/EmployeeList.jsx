import { useDispatch, useSelector } from "react-redux";
import { deleteEmployee } from "../redux/slices/employeeSlice";
import { setLoading } from "../redux/slices/uiSlice";

export default function EmployeeList({ setEditingEmployee }) {
  const dispatch = useDispatch();
  const employees = useSelector((state) => state.employees.employees);

  const handleDelete = (id) => {
    dispatch(setLoading(true));

    setTimeout(() => {
      dispatch(deleteEmployee(id));
      dispatch(setLoading(false));
    }, 400);
  };

  return (
    <div className="card list-card">
      <h2>Employee List</h2>

      {employees.length === 0 ? (
        <p>No employees found.</p>
      ) : (
        <div className="employee-list">
          {employees.map((employee) => (
            <div className="employee-item" key={employee.id}>
              <div className="employee-info">
                <h3>{employee.name}</h3>
                <p>{employee.email}</p>
                <p>{employee.department}</p>
                <p>{employee.designation}</p>
              </div>

              <div className="employee-actions">
                <button onClick={() => setEditingEmployee(employee)}>
                  Edit
                </button>
                <button
                  className="danger-btn"
                  onClick={() => handleDelete(employee.id)}
                >
                  Delete
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}