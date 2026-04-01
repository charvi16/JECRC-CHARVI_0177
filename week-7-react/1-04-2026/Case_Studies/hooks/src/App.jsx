// import { useState } from 'react'
// import './App.css'

// function App() {
//   // const [count, setCount] = useState(0)
//   // const increment = () => {
//   //   setCount(prev => prev + 1);
//   // };

//   // const incrementby2 = () => {
//   //   setCount(prev => prev + 2);
//   // };

//   // const decrement = () => {
//   //    setCount(prev => prev - 1);
//   // };


//   // const resetCount = () => {
//   //   setCount(0);
//   // };

//   // const [user,setUser]=useState({name:" ",age:" "});
//   // const UpdateUserName=()=>{
//   //   setUser(prev=>({...prev,name:"John Doe"}));
//   // }
//   // const UpdateUserAge=(age)=>{
//   //   setUser(prev=>({...prev,age:age}));
//   // }
//   // const resetUser=()=>{
//   //   setUser({name:" ",age:" "});
//   // }


//   // const [data,setData] = useState(()=>{
//   //   console.log("Expensive..");
//   //   let result=0;
//   //   for(let i=0;i<1000000000;i++){
//   //     result+=i;
//   //   }
//   //   return result%1000;

//   // });
//   // const recalculateData=()=>{
//   //   setData(prev=>{
//   //     console.log("Recalculating...");
//   //     return prev+100; 
//   //   });
//   // };

//   // const resetUser = () => {
//   //   setUser({
//   //     name : "",
//   //     age : "",
//   //     email : ""
//   //   });
//   // };

// //   const [user, setUser] = useState({
// //     name: "",
// //     age: "",
// //     email: ""
// //   });

// // const updateUserName = () => {
// //   setUser(prev => ({
// //     ...prev,
// //     name: "John Doe"
// //   }));
// // };

// // const updateUserAge = (value) => {
// //   setUser(prev => ({
// //     ...prev,
// //     age: value
// //   }));
// // };

// // const updateUserEmail = (value) => {
// //   setUser(prev => ({
// //     ...prev,
// //     email: value
// //   }));
// // };

// // const resetUser = () => {
// //   setUser({
// //     name: "",
// //     age: "",
// //     email: ""
// //   });
// // };

// // const [items, setItems] = useState([]);

// //   const addItem = () => {
// //     const newItem = {
// //       id: Date.now(),
// //       name: "Item " + (items.length + 1),
// //       created: new Date().toLocaleTimeString()
// //     };

// //     setItems(prev => [...prev, newItem]);
// //   };

// //   const addMultipleItems = () => {
// //     const newItems = [
// //       {
// //         id: Date.now(),
// //         name: "Batch Item 1",
// //         created: new Date().toLocaleTimeString()
// //       },
// //       {
// //         id: Date.now() + 1,
// //         name: "Batch Item 2",
// //         created: new Date().toLocaleTimeString()
// //       },
// //       {
// //         id: Date.now() + 2,
// //         name: "Batch Item 3",
// //         created: new Date().toLocaleTimeString()
// //       }
// //     ];

// //     setItems(prev => [...prev, ...newItems]);
// //   };
// const [items, setItems] = useState([]);
 
//   const addItem = () => {
//     const newItem = {
//       id: Date.now(),
//       name: "Item " + (items.length + 1),
//       created: new Date().toLocaleTimeString()
//     };

//     setItems(prev => [...prev, newItem]);
//   };

//   const addMultipleItems = () => {
//     const newItems = [
//       {
//         id: Date.now(),
//         name: "Batch Item 1",
//         created: new Date().toLocaleTimeString()
//       },
//       {
//         id: Date.now() + 1,
//         name: "Batch Item 2",
//         created: new Date().toLocaleTimeString()
//       },
//       {
//         id: Date.now() + 2,
//         name: "Batch Item 3",
//         created: new Date().toLocaleTimeString()
//       }
//     ];

//     setItems(prev => [...prev, ...newItems]);
//   };

//   const updateItem = (id) => {
//     setItems(prev =>
//       prev.map(item =>
//         item.id === id
//           ? {
//               ...item,
//               name: "Updated Item",
//               updated: new Date().toLocaleTimeString()
//             }
//           : item
//       )
//     );
//   };

//   const deleteItem = (id) => {
//     setItems(prev => prev.filter(item => item.id !== id));
//   };

//   const deleteAllItems = () => {
//     setItems([]);
//   };


//   return (
//     <>
//       {/* <button onClick={() => setCount(count + 1)}>Increment</button>
//       <button onClick={() => setCount(count + 2)}>Increment By 2</button>
//       <button onClick={() => setCount(count -1)}>Decrement</button> */}

//       {/* <h1>Functional Update Demo </h1>
//       <h2>Count: {count}</h2>
//       <button style={styles.btn} onClick={increment}>
//         Increment
//       </button>
//       <button style={styles.btn} onClick={incrementby2}>
//         Increment by 2
//       </button>
//       <button style={styles.btn} onClick={decrement}>
//         Decrement
//       </button>
//        <button style={styles.btn} onClick={resetCount}>
//         Reset
//       </button> */}

//       {/* <div>
//       <h1>lazy initiliaztion</h1>
//       <h2>Data: {data}</h2>
//       <button onClick={recalculateData}>Recalculate (+100)</button>
//       <p>expensive calculation</p>
//       <p>open console to observe</p>
//     </div> */}

//       {/* <div>
//       <h1>State Update with Objects</h1>
//       <input 
//       type='text'
//       placeholder='enter age'
//       onChange={(e)=>UpdateUserAge(e.target.value)}/>
//       <input 
//       type='text'
//       placeholder='enter name'
//       onChange={(e)=>UpdateUserName(e.target.value)}/>
//       <button onClick={resetUser}>Reset User</button>
//       <h2>User Name: {user.name}</h2>
//       <h2>User Age: {user.age}</h2>
//     </div> */}

//       {/* <div style={styles.contanier}>
//       <h1>Object Update Demo</h1>
//       <input 
//       type="text"
//       placeholder="Enter Age"
//       onChange={(e) => updateUserAge(e.target.value)}
//       style={styles.input}/>

//       <input 
//       type="email"
//       placeholder="Enter Email"
//       onChange={(e) => updateUserEmail(e.target.value)}
//       style={styles.input}/>

//       <div> 
//         <button style={styles.btn} onClick={updateUserName}>Set Name</button>
//         <button style={styles.btn} onClick={resetUser}>Reset User</button>
//       </div>
//       <div style={styles.card}>
//         <h3>User Details</h3>
//         <p><b>Name:</b>{user.name}</p>
//         <p><b>Age:</b>{user.age}</p>
//         <p><b>Email:</b>{user.email}</p>
//       </div>
//     </div> */}


//       {/* <div style={{ padding: "20px" }}>
//       <h2>Items List</h2>

//       <button onClick={addItem}>Add Item</button>
//       <button onClick={addMultipleItems} style={{ marginLeft: "10px" }}>
//         Add Multiple Items
//       </button>

//       <ul>
//         {items.map(item => (
//           <li key={item.id}>
//             {item.name} - {item.created}
//           </li>
//         ))}
//       </ul>
//     </div> */}

//     <div style={{ padding: "20px" }}>
//       <h2>Items List</h2>

//       <button onClick={addItem}>Add Item</button>
//       <button onClick={addMultipleItems} style={{ marginLeft: "10px" }}>
//         Add Multiple Items
//       </button>
//       <button onClick={deleteAllItems} style={{ marginLeft: "10px" }}>
//         Delete All
//       </button>

//       <ul>
//         {items.map(item => (
//           <li key={item.id}>
//             {item.name} - {item.created}

//             <button
//               onClick={() => updateItem(item.id)}
//               style={{ marginLeft: "10px" }}
//             >
//               Update
//             </button>

//             <button
//               onClick={() => deleteItem(item.id)}
//               style={{ marginLeft: "5px" }}
//             >
//               Delete
//             </button>

//             {item.updated && (
//               <span style={{ marginLeft: "10px", color: "green" }}>
//                 (Updated: {item.updated})
//               </span>
//             )}
//           </li>
//         ))}
//       </ul>
//     </div>


//     </>
//   )
// }

// // const styles = {
// //   container: {
// //     textAlign: 'center',
// //     marginTop: '50px',
// //   },
// //   btn: {
// //     margin: '10px',
// //     padding: '10px 20px',
// //     fontSize: '16px',
// //   },
// // };

// const styles ={
//   contanier: {
//     textAlign: "center",
//     marginTop: "50px"
//   },
//   btn: {
//     margin: "10px",
//     padding: "10px 20px",
//     fontSize: "16px"
//   },
//   card: {
//     margin: "20px auto",
//     padding: "20px",
//     width: "300px",
//     border: "1px solid #ccc",
//     borderRadius: "8px",
//     textAlign: "left"
//   },
//   input: {
//     margin: "10px",
//     padding: "10px",
//     fontSize: "16px",
//     width: "200px"
//   }
// };


// export default App


import React, { useReducer, useState } from "react";

function App() {

  // 🔹 Initial State
  const initialCounterState = {
    count: 0,
    history: []
  };

  // 🔹 Reducer Function
  function counterReducer(state, action) {
    switch (action.type) {
      case "increment":
        return {
          count: state.count + 1,
          history: [
            ...state.history,
            { type: "increment", value: state.count + 1, time: new Date().toLocaleTimeString() }
          ]
        };

      case "decrement":
        return {
          count: state.count - 1,
          history: [
            ...state.history,
            { type: "decrement", value: state.count - 1, time: new Date().toLocaleTimeString() }
          ]
        };

      case "reset":
        return {
          count: 0,
          history: [
            ...state.history,
            { type: "reset", value: 0, time: new Date().toLocaleTimeString() }
          ]
        };

      case "set":
        return {
          count: action.payload,
          history: [
            ...state.history,
            { type: "set", value: action.payload, time: new Date().toLocaleTimeString() }
          ]
        };

      default:
        return state;
    }
  }

  // 🔹 useReducer Hook
  const [counterState, dispatch] = useReducer(counterReducer, initialCounterState);

  // 🔹 Input State for SET
  const [inputValue, setInputValue] = useState("");

  return (
    <div style={styles.container}>
      <h1>useReducer Counter (Advanced)</h1>

      <h2>Count: {counterState.count}</h2>

      {/* 🔹 Actions */}
      <div>
        <button style={styles.btn} onClick={() => dispatch({ type: "increment" })}>
          +1
        </button>

        <button style={styles.btn} onClick={() => dispatch({ type: "decrement" })}>
          -1
        </button>

        <button style={styles.resetBtn} onClick={() => dispatch({ type: "reset" })}>
          Reset
        </button>
      </div>

      {/* 🔹 Set Value */}
      <div style={{ marginTop: "20px" }}>
        <input
          type="number"
          placeholder="Enter value"
          value={inputValue}
          onChange={(e) => setInputValue(e.target.value)}
          style={styles.input}
        />

        <button
          style={styles.btn}
          onClick={() =>
            dispatch({ type: "set", payload: Number(inputValue) })
          }
        >
          Set Value
        </button>
      </div>

      {/* 🔹 History */}
      <h3 style={{ marginTop: "30px" }}>History</h3>

      <ul style={styles.list}>
        {counterState.history.map((item, index) => (
          <li key={index} style={styles.card}>
            <b>{item.type.toUpperCase()}</b> → {item.value}
            <br />
            <small>{item.time}</small>
          </li>
        ))}
      </ul>

      <p style={styles.info}>
        👉 useReducer is best for <b>complex state logic & history tracking</b>
      </p>
    </div>
  );
}

// 🎨 Styling
const styles = {
  container: {
    textAlign: "center",
    marginTop: "40px",
    fontFamily: "Arial"
  },
  btn: {
    margin: "10px",
    padding: "10px 15px",
    cursor: "pointer"
  },
  resetBtn: {
    margin: "10px",
    padding: "10px 15px",
    backgroundColor: "red",
    color: "white",
    border: "none",
    cursor: "pointer"
  },
  input: {
    padding: "10px",
    marginRight: "10px"
  },
  list: {
    listStyle: "none",
    padding: 0
  },
  card: {
    border: "1px solid #ccc",
    margin: "10px auto",
    padding: "10px",
    width: "250px"
  },
  info: {
    marginTop: "20px",
    color: "green"
  }
};

export default App;