import React from "react";
import { useDispatch } from "react-redux";
import { addEmployee } from "../app/employeeSlice";
const EmployeeAdd = () => {
    const dispatch = useDispatch();
    const [name, setName] = React.useState('');
    const [position, setPosition] = React.useState('');
    const handleSubmit = (e) => {
        e.preventDefault();
        const newEmployee = {
            id: Date.now(),
            name,
            position
        };
        dispatch(addEmployee(newEmployee));
        setName('');
        setPosition('');
    };
    return (
        <div>
            <h2>Add Employee</h2>
            <form onSubmit={handleSubmit}>
                <input 
                    type="text"
                    placeholder="Name "
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
                <input 
                    type="text"
                    placeholder="Position"
                    value={position}
                    onChange={(e) => setPosition(e.target.value)}
                />
                <button type="submit">Add Employee</button>
            </form>
        </div>
    );
};

export default EmployeeAdd;