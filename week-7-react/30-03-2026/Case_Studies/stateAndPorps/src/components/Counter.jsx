import React from 'react'
import { useState } from 'react'

function Counter() {
    const [count, setCount] = useState(0);
    const [step , setStep] = useState(1);
    const [lastActive, setLastActive] = useState(0); 
    const increment = () => {
        setCount(count + step);

        setLastActive("Incremented by : " + step);
    }

    const decrement = () => {
       setCount(count - step);

        setLastActive("Decremented by : " + step);
        
    }

    const reset = () => {
      setCount(0);
      setLastActive("Reset to : " + count);
    }
  return (
    <div className='container' style={{padding:'3vh', textAlign:'center'}}>

        <div style={{fontSize : '4vh', margin:'1vh'}}>
          <h1>Counter : {count}</h1>
        </div>

        <div style={{marginBottom : '3vh'}}>
          <label> Step : 
            <input
              type='number'
              value={step}
              onChange={(e) => setStep(Number(e.target.value))}
              style={{marginLeft : '5vh', width : '3vh'}}
              ></input>
          </label>
        </div>

        <div>
          <button onClick={increment} style={buttonStyle}>increment</button>
          <button onClick={decrement} style={buttonStyle}>decrement</button>
          <button onClick={reset} style={buttonStyle}>reset</button>

        </div>

        <div style={{marginTop : "2vh", fontStyle : ''}}>
          Last Action : {lastActive}
        </div>
    </div>
  );
}

const buttonStyle = {
  margin: '2vw, 4vh',
  padding : '3vh 4vh',
  fontSize : '4vh',
  cursor : 'pointer',
  backgroundColor : 'pink',
  color : 'white',
  border : 'none',
  borderRadius : '2vh'
}

export default Counter;