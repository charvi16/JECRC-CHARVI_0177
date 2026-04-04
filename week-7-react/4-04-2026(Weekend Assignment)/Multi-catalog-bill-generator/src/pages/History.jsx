import { useEffect, useMemo, useState } from "react";
import { getInvoices } from "../api";
import { exportInvoicePdf } from "../utils/pdf";
import { exportInvoiceCsv } from "../utils/csv";

export default function History() {
  const [invoices, setInvoices] = useState([]);
  const [search, setSearch] = useState("");
  const [statusFilter, setStatusFilter] = useState("");
  const [dateFilter, setDateFilter] = useState("");

  useEffect(() => {
    loadInvoices();
  }, []);

  async function loadInvoices() {
    try {
      const data = await getInvoices();
      setInvoices(data);
    } catch (error) {
      console.error(error);
    }
  }

  const filtered = useMemo(() => {
    return invoices.filter((invoice) => {
      const matchesSearch =
        invoice.invoiceNumber.toLowerCase().includes(search.toLowerCase());

      const matchesStatus = statusFilter
        ? invoice.status === statusFilter
        : true;

      const matchesDate = dateFilter
        ? invoice.createdAt.slice(0, 10) === dateFilter
        : true;

      return matchesSearch && matchesStatus && matchesDate;
    });
  }, [invoices, search, statusFilter, dateFilter]);

  return (
    <div className="page-grid">
      <div className="panel">
        <div className="panel-title-row">
          <h2>Invoice History</h2>
        </div>

        <div className="filter-row">
          <input
            placeholder="Search by invoice number"
            value={search}
            onChange={(e) => setSearch(e.target.value)}
          />

          <select
            value={statusFilter}
            onChange={(e) => setStatusFilter(e.target.value)}
          >
            <option value="">All Status</option>
            <option value="Finalized">Finalized</option>
          </select>

          <input
            type="date"
            value={dateFilter}
            onChange={(e) => setDateFilter(e.target.value)}
          />

          <button className="secondary-btn" onClick={loadInvoices}>
            Refresh
          </button>
        </div>
      </div>

      <div className="history-list">
        {filtered.length === 0 ? (
          <div className="panel empty">No invoices found.</div>
        ) : (
          filtered.map((invoice) => (
            <div className="history-card" key={invoice.id}>
              <div className="panel-title-row">
                <strong>{invoice.invoiceNumber}</strong>
                <span className="badge">{invoice.status}</span>
              </div>

              <div className="history-meta">
                <span>{new Date(invoice.createdAt).toLocaleString()}</span>
                <span>Total: ₹{invoice.grandTotal.toFixed(2)}</span>
              </div>

              <div className="invoice-preview-items">
                {invoice.items.map((item) => (
                  <div key={item.id}>
                    {item.itemName} · Qty {item.quantity} · ₹{item.unitPrice}
                  </div>
                ))}
              </div>

              <div className="actions-box" style={{ marginTop: 12 }}>
                <button
                  className="secondary-btn"
                  onClick={() => exportInvoicePdf(invoice)}
                >
                  PDF
                </button>
                <button
                  className="secondary-btn"
                  onClick={() => exportInvoiceCsv(invoice)}
                >
                  CSV
                </button>
              </div>
            </div>
          ))
        )}
      </div>
    </div>
  );
}