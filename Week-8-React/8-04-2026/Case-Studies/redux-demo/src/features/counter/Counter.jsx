import React from 'react'
import {useSelector, useDispatch} from 'react-redux';
import {increment, decrement} from './CounterSlice';

function Counter() {
    const state = useSelector((state) => state.counter.value)
    const dispatch = useDispatch();
  return (
    <div>
        <h1>Counter :</h1>
        <button onClick={() => dispatch(increment())}>Increment</button>
        <button onClick={() => dispatch(decrement())}>Decrement</button>

    </div>
  )
}

export default Counter