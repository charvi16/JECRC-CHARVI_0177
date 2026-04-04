import React, { forwardRef } from "react";

const PrintInvoice = forwardRef(({ invoice }, ref) => {
    
  return (
    <div ref={ref} style={{ padding: "20px", fontFamily: "Arial" }}>
      <h2>Invoice</h2>

      <p><strong>Invoice No:</strong> {invoice.invoiceNumber}</p>
      <p><strong>Date:</strong> {new Date().toLocaleString()}</p>

      <table width="100%" border="1" cellPadding="8" style={{ borderCollapse: "collapse" }}>
        <thead>
          <tr>
            <th>Item</th>
            <th>Qty</th>
            <th>Price</th>
            <th>Tax %</th>
            <th>Total</th>
          </tr>
        </thead>
        <tbody>
          {invoice.items.map((item, i) => (
            <tr key={i}>
              <td>{item.name}</td>
              <td>{item.qty}</td>
              <td>{item.price}</td>
              <td>{item.tax}</td>
              <td>
                {(item.price * item.qty + (item.price * item.qty * item.tax) / 100).toFixed(2)}
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <h3>Total: ₹{invoice.total.toFixed(2)}</h3>
    </div>
  );
});

export default PrintInvoice;