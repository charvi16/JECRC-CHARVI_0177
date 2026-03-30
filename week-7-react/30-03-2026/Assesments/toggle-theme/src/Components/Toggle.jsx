import React from 'react'
import { useState } from 'react'

function Toggle() {
    const [theme, setTheme] = useState(true);

    const change = () => {
        setTheme(!theme);
    }

  return (
    <div className='container'
    style={{
        ...(theme ? light : dark),
        height: "100vh",
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center"
      }}
    >

        <h1>Mode : {theme ? "Light" : "Dark"}</h1>
        <button onClick={change} style={{ padding: "10px 20px", marginTop: "20px" }}>switch to {theme ? "Dark" : "Light"} Mode</button>
    </div>
  )
}

const light = {
    backgroundColor : 'pink',
    color : 'white'
}

const dark = {
    backgroundColor : 'black',
    color : 'white'
}

export default Toggle