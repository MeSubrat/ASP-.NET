import React, { useState } from 'react'
import { FaStar } from "react-icons/fa"
import './style.css'
function StarRating({ noOfStars = 5 }) {
    const [rating, setRating] = useState(0)
    const [hover, setHover] = useState(0);

    function handleClick(getCurrentIndex) {
        setRating(getCurrentIndex)
    }
    function handleHover(getCurrentIndex) {
        // console.log(getCurrentIndex)
        setHover(getCurrentIndex)
    }
    function handleLeave() {
        // console.log(getCurrentIndex)
        setHover(rating)
    }

    return (
        <div className='star-rating'>
            {[...Array(noOfStars)].map((_,index)=>{
                index+=1;
                return(
                    <FaStar
                    className={index<= (hover || rating )?'active':'inactive'}
                    key={index}
                    onClick={()=>handleClick(index)}
                    onMouseEnter={()=>handleHover(index)}
                    onMouseLeave={handleLeave}
                    size={40}
                    />
                );
            })}
        </div>
    )


}

export default StarRating;