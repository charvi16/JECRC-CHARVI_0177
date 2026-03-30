import React from 'react'
import { useState } from "react";

function Todo() {
    const [task, setTask] = useState("");
    const [todos, setTodos] = useState([]);

    const addTask = () => {
    if (task.trim() === "") return;

    const newTodo = {
      id: Date.now(),
      text: task,
      completed: false,
    };

    setTodos([...todos, newTodo]); 
    setTask("");
  };

  const deleteTask = (id) => {
    const updated = todos.filter((todo) => todo.id !== id);
    setTodos(updated);
  };

  const toggleTask = (id) => {
    const updated = todos.map((todo) =>
      todo.id === id
        ? { ...todo, completed: !todo.completed }
        : todo
    );

    setTodos(updated);
  };


  return (
     <div style={{ padding: "40px", textAlign: "center" }}>
      <h1>Todo App</h1>

      {/* Input */}
      <input
        type="text"
        placeholder="Enter Task"
        value={task}
        onChange={(e) => setTask(e.target.value)}
      />

      <button onClick={addTask} style={{ marginLeft: "10px" }}>
        Add
      </button>

      {/* List */}
      <div style={{ marginTop: "20px" }}>
        {todos.map((todo) => (
          <div key={todo.id} style={{ margin: "10px" }}>
            <span
              onClick={() => toggleTask(todo.id)}
              style={{
                cursor: "pointer",
                textDecoration: todo.completed ? "line-through" : "none",
                marginRight: "10px",
              }}
            >
              {todo.completed ? "☑" : "☐"} {todo.text}
            </span>

            <button onClick={() => deleteTask(todo.id)}>X</button>
          </div>
        ))}
      </div>
    </div>
  )
}

export default Todo