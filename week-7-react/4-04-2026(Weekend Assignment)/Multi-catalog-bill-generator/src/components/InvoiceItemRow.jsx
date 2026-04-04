import { useBilling } from "../context/BillingContext";

export default function InvoiceItemRow({ item }) {
  const { removeItem, updatePrice, updateQty, updateTax } = useBilling();

  return (
    <div className="invoice-row">
      <div>
        <strong>{item.name}</strong>
        <div className="item-meta">{item.catalogType}</div>
      </div>

      <div className="qty-box">
        <button
          className="qty-btn"
          onClick={() => updateQty(item.id, item.qty - 1)}
        >
          -
        </button>
        <span className="qty-value">{item.qty}</span>
        <button
          className="qty-btn"
          onClick={() => updateQty(item.id, item.qty + 1)}
        >
          +
        </button>
      </div>

      <input
        className="small-input"
        type="number"
        value={item.price}
        onChange={(e) => updatePrice(item.id, e.target.value)}
      />

      <input
        className="small-input"
        type="number"
        value={item.tax}
        onChange={(e) => updateTax(item.id, e.target.value)}
      />

      <div>
        ₹{(item.price * item.qty + (item.price * item.qty * item.tax) / 100).toFixed(2)}
      </div>

      <button className="danger-btn" onClick={() => removeItem(item.id)}>
        ✕
      </button>
    </div>
  );
}