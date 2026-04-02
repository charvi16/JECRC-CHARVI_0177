import React from 'react';
import './App.css';
import Layout from './components/Layout';
import About from './pages/About';
import Contact from './pages/Contact';
import Home from './pages/Home';
import { Route, Routes } from 'react-router-dom';

function App() {
  return (
    <Routes>

      
      <Route path="/" element={<Layout />}>

        
        <Route index element={<Home />} />
        <Route path="Contact" element={<Contact />} />
        <Route path="About" element={<About />} />

      </Route>

    </Routes>
  );
}

export default App;