import React from "react";
import { useOutletContext } from "react-router-dom";

function ProductSpecs() {
  const { product } = useOutletContext();

  return (
    <div>
      <h2>Product Specifications</h2>
      <ul className="nested-list">
        {product.specs.map((spec, index) => (
          <li key={index}>{spec}</li>
        ))}
      </ul>
    </div>
  );
}

export default ProductSpecs;