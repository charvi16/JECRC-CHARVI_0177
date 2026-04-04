import { useBilling } from "../context/BillingContext";

export default function DiscountBox() {
  const { discount, setDiscount } = useBilling();

  return (
    <div className="discount-box">
      <h3>Discount</h3>
      <div className="custom-form" style={{ marginTop: 10 }}>
        <select
          value={discount.type}
          onChange={(e) =>
            setDiscount((prev) => ({ ...prev, type: e.target.value }))
          }
        >
          <option value="none">No Discount</option>
          <option value="percent">Percentage</option>
          <option value="fixed">Fixed Amount</option>
        </select>

        <input
          type="number"
          placeholder="Discount value"
          value={discount.value}
          onChange={(e) =>
            setDiscount((prev) => ({ ...prev, value: Number(e.target.value) }))
          }
        />
      </div>
    </div>
  );
}