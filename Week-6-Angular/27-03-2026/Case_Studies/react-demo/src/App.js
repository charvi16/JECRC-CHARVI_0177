// import logo from './logo.svg';
// import './App.css';

// function App() {
//   return (
//     <div className="App">
//       <header className="App-header">
//         <img src={logo} className="App-logo" alt="logo" />
//         <p>
//           Edit <code>src/App.js</code> and save to reload.
//         </p>
//         <a
//           className="App-link"
//           href="https://reactjs.org"
//           target="_blank"
//           rel="noopener noreferrer"
//         >
//           Learn React
//         </a>
//       </header>
//     </div>
//   );
// }

// function App() {
//   const userName = "Raghav";
//   const userRole = "Admin";
//   const isLoggedIn = true;
//   const unreadMessages = 3;

//   const getGreeting = () => {
//     return "Hello";
//   };

//   const notificationBadge =
//     unreadMessages > 0 ? (
//       <span className="badge">{unreadMessages}</span>
//     ) : null;

//   return (
//     <div>
//       <h1>
//         {getGreeting()}, {userName}
//       </h1>

//       <p>Your role: {userRole}</p>

//       {isLoggedIn ? (
//         <div>
//           <p>You have {unreadMessages} unread messages.</p>
//           {notificationBadge}
//         </div>
//       ) : (
//         <p>Please log in to see your messages.</p>
//       )}

//       {/* List rendering example */}
//       <ul>
//         {["Learn React", "Build a project", "Deploy to production"].map(
//           (task, index) => (
//             <li key={index}>{task}</li>
//           )
//         )}
//       </ul>
//     </div>
//   );
// }



// export default App;


// import { useState } from "react";
// import {Header} from './components/Header';
// // import {Card} from './components/Card';

// function App(){
//   const [count, setCount] = useState(0);
//   const [timeStamp, setTimeStamp] = useState(new Date().toLocaleTimeString());

//   const updateTimeStamp = () => {
//     setTimeStamp(new Date().toLocaleTimeString());
//   };
//   return(
//     <div>
//       <Header
//       title = "Component Composition Demo"
//       subtitle = "Building resuable components"
//       />
//       <h1>Virtual DOM</h1>
//       <div style={{padding:'20px', border:'1px solid #ccc' }}>
//         <h2>Counter : {count} </h2>
//         <button onClick={() => { setCount(count + 1)}}>INcrement btn</button>
//       </div>

//       <div style={{padding : '20px', marginTop: '20px', vorder : '1px solid #ccc'}}>
//       <h2>TimeStamp : {timeStamp}</h2>
//       <button onClick={updateTimeStamp}>UPdate time</button>
//       </div>

//       <p style={{color: 'gray'}}>Static content, react soesnt touch this</p>

//       {/* </Card> */}
//     </div>
//   )
// }

// export default App;

// import React from "react";
// import Header from "./components/Header";
// import Card from "./components/Card";
// import UserProfile from "./components/UserProfile";

// function App() {
//   const projects = [
//     {id: 1, title: "Project Alpha", content: "A cutting-edge AI project.", icon: "🤖", isFeatured: true},
//     {id: 2, title: "Project Beta", content: "A innovative web development project.", icon: "💻", isFeatured: false},
//     {id: 3, title: "Project Gamma", content: "A groundbreaking mobile app project.", icon: "📱", isFeatured: true}
//   ];

//   const handleEdit = () =>{
//     alert("Edit Profile CLicked");
//   }

//   const somes = [
//     {name : "chars", age : 21, email : "jrbek@gmail.com", isActive : true, hobbies : [
//       "jfke", "jnwekl", "cdke", "ncke"
//     ], onEdit:() => alert("Edit clicked!")} ,
//     {name : "chars", age : 21, email : "jrbek@gmail.com", isActive : false, hobbies : [
//       "jfke", "jnwekl", "cdke", "ncke"
//     ], onEdit:() => alert("Edit clicked!")} ,
//     {name : "chars", age : 21, email : "jrbek@gmail.com", isActive : true, hobbies : [
//       "jfke", "jnwekl", "cdke", "ncke"
//     ], /*onEdit:() => alert("Edit clicked!")*/ onEdit:{handleEdit}} 
//   ];

  

//   return (
//     <div>
//       <Header 
//       title="component composition demo"
//       subtitle="Building UIs with reusable pieces"
//       />
//       <div style={{ 
//         display: 'flex',
//         flexWrap: 'wrap',
//         justifyContent: 'center',
//         padding: '20px' }}>
//         {projects.map((project) => (
//           <Card
//             key={project.id}
//             title={project.title}
//             content={project.content}
//             icon={project.icon}
//             isFeatured={project.isFeatured}
//           />
//         ))}
//       </div>
//       {somes.map((some) => (
//         <UserProfile
//         name={some.name}
//         age={some.age}
//         email={some.email}
//         isActive={some.isActive}
//         hobbies={some.hobbies}
//         onEdit={some.onEdit}/>
//       ))}

//     </div>
//   );
// }

// export default App;

import React, { useState } from 'react';
import TodoForm from './components/TodoForm';
import TodoItem from './components/TodoItem';
import TodoStats from './components/ToDoStats';

function App() {
  const [todos, setTodos] = useState([
    { id: 1, text: 'Learn React Props', completed: true },
    { id: 2, text: 'Build a Todo App', completed: false },
    { id: 3, text: 'Master Component Communication', completed: false }
  ]);
  
  // Add new todo - receives data from child (TodoForm)
  const addTodo = (text) => {
    const newTodo = {
      id: Date.now(),
      text: text,
      completed: false
    };
    setTodos([...todos, newTodo]);
  };
  
  // Toggle todo status - receives data from child (TodoItem)
  const toggleTodo = (id) => {
    setTodos(todos.map(todo =>
      todo.id === id ? { ...todo, completed: !todo.completed } : todo
    ));
  };
  
  // Delete todo - receives data from child (TodoItem)
  const deleteTodo = (id) => {
    setTodos(todos.filter(todo => todo.id !== id));
  };
  
  return (
    <div style={{ maxWidth: '600px', margin: '0 auto', padding: '20px' }}>
      <h1>📝 Todo App - Communication Patterns</h1>
      <p style={{ color: '#666' }}>
        <strong>Patterns shown:</strong><br/>
        • Parent → Child: Props passed to TodoForm, TodoItem, TodoStats<br/>
        • Child → Parent: Callbacks (addTodo, toggleTodo, deleteTodo)<br/>
        • Sibling Communication: TodoForm updates state, TodoStats displays it
      </p>
      
      {/* Child to Parent: TodoForm sends data UP via onAddTodo */}
      <TodoForm onAddTodo={addTodo} />
      
      {/* Parent to Child: Stats receives todos via props */}
      <TodoStats todos={todos} />
      
      {/* Parent to Child: TodoItem receives data and callbacks */}
      <div>
        <h3>Your Tasks</h3>
        {todos.length === 0 ? (
          <p>No tasks yet. Add one above!</p>
        ) : (
          todos.map(todo => (
            <TodoItem
              key={todo.id}
              todo={todo}
              onToggle={toggleTodo}
              onDelete={deleteTodo}
            />
          ))
        )}
      </div>
    </div>
  );
}

export default App;