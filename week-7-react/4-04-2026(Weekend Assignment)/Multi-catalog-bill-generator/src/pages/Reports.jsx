import { useEffect, useState } from "react";
import { getDailySummary } from "../api";

export default function Reports() {
  const [date, setDate] = useState(new Date().toISOString().slice(0, 10));
  const [summary, setSummary] = useState(null);

  useEffect(() => {
    loadSummary(date);
  }, [date]);

  async function loadSummary(selectedDate) {
    try {
      const data = await getDailySummary(selectedDate);
      setSummary(data);
    } catch (error) {
      console.error(error);
    }
  }

  return (
    <div className="page-grid">
      <div className="panel">
        <div className="panel-title-row">
          <h2>Daily Sales Summary</h2>
          <input
            type="date"
            value={date}
            onChange={(e) => setDate(e.target.value)}
            style={{ maxWidth: 220 }}
          />
        </div>
      </div>

      {summary && (
        <div className="report-grid">
          <div className="report-card">
            <h3>Total Bills</h3>
            <p>{summary.totalBills}</p>
          </div>

          <div className="report-card">
            <h3>Total Revenue</h3>
            <p>₹{summary.totalRevenue.toFixed(2)}</p>
          </div>

          <div className="report-card">
            <h3>Total Tax</h3>
            <p>₹{summary.totalTax.toFixed(2)}</p>
          </div>

          <div className="report-card">
            <h3>Total Discount</h3>
            <p>₹{summary.totalDiscount.toFixed(2)}</p>
          </div>
        </div>
      )}
    </div>
  );
}