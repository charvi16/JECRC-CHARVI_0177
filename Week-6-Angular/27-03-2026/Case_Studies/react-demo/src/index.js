import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';

// function App(){
//   return (
//     <div>
//       <h1>Welcome to the React Training</h1>
//       <p>This is rendered by React, not Vanilla JavaScript</p>
//     </div>
//   );
// }

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);