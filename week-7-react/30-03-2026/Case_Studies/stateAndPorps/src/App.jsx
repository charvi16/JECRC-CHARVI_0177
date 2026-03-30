import { useState } from 'react'
import Counter from './components/Counter';
import './App.css'

function App() {
  const [count, setCount] = useState(10)

  return (
    <>
      <Counter/>
    </>
  )
}

export default App
