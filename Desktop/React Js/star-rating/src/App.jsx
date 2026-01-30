import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import StarRating from './components/starRating'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
    <h2>Welcome TO Star Rating</h2>
    <StarRating noOfStars={7}/>
    </>
  )
}

export default App
