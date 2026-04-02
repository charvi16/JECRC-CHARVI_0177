import React from 'react'

function Contact() {
  return (
    <div style={styles.container}>
        <h1>COntact Page</h1>
        <p>Yuo can reach us ar :</p>
        <p>Email : support@somethingRandom.com</p>
        <p>Phone +9876543210</p>
    </div>
  )
}
const styles = {
    container : {
        textAlign : "center",
        padding : "3vh",
        background : "#d4edda"
    }
}

export default Contact