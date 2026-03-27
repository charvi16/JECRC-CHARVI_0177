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


import { useState } from "react";

function App(){
  const [count, setCount] = useState(0);
  const [timeStamp, setTimeStamp] = useState(new Date().toLocaleTimeString());

  const updateTimeStamp = () => {
    setTimeStamp(new Date().toLocaleTimeString());
  };
  return(
    <div>
      <h1>Virtual DOM</h1>
      <div style={{padding:'20px', border:'1px solid #ccc' }}>
        <h2>Counter : {count} </h2>
        <button onClick={() => { setCount(count + 1)}}>INcrement btn</button>
      </div>

      <div style={{padding : '20px', marginTop: '20px', vorder : '1px solid #ccc'}}>
      <h2>TimeStamp : {timeStamp}</h2>
      <button onClick={updateTimeStamp}>UPdate time</button>
      </div>

      <p style={{color: 'gray'}}>Static content, react soesnt touch this</p>
    </div>
  )
}

export default App;