import React from 'react'
import { Navigate } from 'react-router-dom'
import { useAuth } from '../context/authContext'

function ProtectedRoute({ children }) {
  const { isAuthenticated, loading } = useAuth()

  if (loading) {
    return <p>Loading...</p>
  }

  if (!isAuthenticated) {
    return <Navigate to="/" /> 
  }

  return children
}

export default ProtectedRoute