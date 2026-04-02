import React from 'react'
import Navbar from './Navbar'
import {Outlet} from 'react-router-dom';

function Layout() {
  return (
    <>
    <Navbar/>
    <div style={styles.container}>
        <Outlet/>
    </div>
    </>
  )
}

const styles = {
    container : {
        padding : "5vh",
        textAlign : "center"
    }
}

export default Layout