export default function Header({ page, setPage }) {
  return (
    <div className="header">
      <div className="brand">
        <h1>Multi-Catalog Bill Generator</h1>
        <p>Fast billing for tickets, donations, products, and custom items.</p>
      </div>

      <div className="nav-buttons">
        <button
          className={`nav-btn ${page === "billing" ? "active" : ""}`}
          onClick={() => setPage("billing")}
        >
          Billing
        </button>
        <button
          className={`nav-btn ${page === "history" ? "active" : ""}`}
          onClick={() => setPage("history")}
        >
          History
        </button>
        <button
          className={`nav-btn ${page === "reports" ? "active" : ""}`}
          onClick={() => setPage("reports")}
        >
          Reports
        </button>
      </div>
    </div>
  );
}