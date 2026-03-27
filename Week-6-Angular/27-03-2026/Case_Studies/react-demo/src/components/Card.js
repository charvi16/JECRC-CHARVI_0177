import React from 'react'

function Card({title, content, icon, isFeatured = false}) {
  return (
    <div style={{
        width : '300px',
        boxShadow : '0 2px 4px rgba(0,0,0.1)',
        backgroundColor : 'white',
        margin : '10px',
        padding : '20px',
        borderRadius : '8px',
        border : isFeatured ? '2px solid gold' : '1px solid #ddd',

    }}>
        {icon && <div style={{fontSize:'20px'}}>{icon}</div>}
        <h3>{title}</h3>
        <p>{content}</p>
    </div>
  )
}

export default Card