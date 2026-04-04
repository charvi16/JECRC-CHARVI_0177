export default function SummaryBox({ totals }) {
  return (
    <div className="summary-box">
      <h3>Summary</h3>

      <div className="summary-line">
        <span>Subtotal</span>
        <span>₹{totals.subtotal.toFixed(2)}</span>
      </div>

      <div className="summary-line">
        <span>Tax</span>
        <span>₹{totals.taxAmount.toFixed(2)}</span>
      </div>

      <div className="summary-line">
        <span>Discount</span>
        <span>- ₹{totals.discountAmount.toFixed(2)}</span>
      </div>

      <div className="summary-total">
        <span>Grand Total</span>
        <span>₹{totals.total.toFixed(2)}</span>
      </div>
    </div>
  );
}