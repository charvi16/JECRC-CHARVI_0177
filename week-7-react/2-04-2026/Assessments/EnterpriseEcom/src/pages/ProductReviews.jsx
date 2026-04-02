import React from "react";
import { useOutletContext } from "react-router-dom";

function ProductReviews() {
  const { product } = useOutletContext();

  return (
    <div>
      <h2>Customer Reviews</h2>
      <ul className="nested-list">
        {product.reviews.map((review, index) => (
          <li key={index}>{review}</li>
        ))}
      </ul>
    </div>
  );
}

export default ProductReviews;