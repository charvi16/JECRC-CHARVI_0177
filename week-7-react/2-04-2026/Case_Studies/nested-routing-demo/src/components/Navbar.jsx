import React from 'react'
import { NavLink } from 'react-router-dom';

function Navbar() {
  return (
    <nav style={styles.nav}>
        <div>
            <NavLink to="/" style={styles.link} end>Home</NavLink>
            <NavLink to="/About" style={styles.link} end>About</NavLink>
            <NavLink to="/Contact" style={styles.link} end>Contact</NavLink>
        </div>
    </nav>
  )
}

const styles = {
    nav : {
        display : "flex",
        justifyContent : "center",
        padding : "4vw 7vh",
        background : "#1e293b",
        color : "white",
        alignItems : "center"
    },
    logo : {
        margin : 0
    },
    link : ({isActive}) => ({
        margin : "0 3vh",
        textDecoration : "none",
        color : isActive ? "#38bdf8" : "white",
        fontWeight : isActive ? "bold" : "normal"
    })
}

export default Navbar