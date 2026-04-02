import React from 'react'

function About() {
  return (
    <div style={styles.container}>
        <h1>About Page</h1>
        <p>This application demonstrates react router concepts.</p>
        <p>It includes navigation, routing and component rendering</p>
    </div>
  )
}
const styles = {
    container : {
        textAlign : "center",
        padding : "3vh",
        background : "#fff3cd"
    }
}

export default About