import React from 'react'
import { useState } from 'react'

function DisplayCard({title, value, onChange}) {
    const [internalCount, setInternalCount] = useState(0);

  return (
    <div style={{border : "2vh solid #ccc", borderRadius : "1vh", width : '50vw', textAlign : 'center'}}>
        <h3>{title}</h3>
        <p>{value}</p>
        <button onClick={() => setInternalCount(internalCount + 2)}>Update Internal Count</button>
        <button onClick={() => onChange(value + 1)}>Update Parent Count</button>

    </div>
  );
}

function StateVsProps(){
    const [parentCount, setParentCount] = useState(0);
    const [parentStep, setParentStep] = useState(1);
    const [displayColor, setDisplayColor] = useState('lightblue');
    
    const handleParentCounterChange = (newCount) => {
        setParentCount(newCount);
        setDisplayColor(newCount % 2 === 0 ? 'lightblue' : 'lightcoral');
    }
    return(
       <div style={{display: 'flex', justifyContent: 'center', padding: '20px'}}>
        <p>Parent Count: {parentCount}</p>
        <button onClick={() => setParentStep(parentStep + 1)} 
        style={{marginLeft: '20px'}}>
            Increase Step (Current: {parentStep})</button>
        <button onClick={() => setDisplayColor
            (displayColor === 'lightblue' ? 'lightcoral' : 'lightblue')} 
            style={{marginLeft: '20px'}}>
            Toggle Display Color</button>
        <DisplayCard
            title="Counter Card" 
            value={parentCount} 
            onChange={handleParentCounterChange} 
            style={{backgroundColor: displayColor}}
        />

        <DisplayCard
            title="Counter Card child component 2" 
            value={parentCount} 
            onChange={handleParentCounterChange} 
            style={{backgroundColor: displayColor}}
        />
        </div>
    )
}

export default StateVsProps