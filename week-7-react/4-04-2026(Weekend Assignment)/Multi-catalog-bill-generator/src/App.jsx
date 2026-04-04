import { useEffect, useState } from "react";
import Header from "./components/Header";
import CatalogPanel from "./components/CatalogPanel";
import InvoicePanel from "./components/InvoicePanel";
import History from "./pages/History";
import Reports from "./pages/Reports";
import { loadDraft } from "./utils/storage";
import { useBilling } from "./context/BillingContext";

export default function App() {
  const [page, setPage] = useState("billing");
  const { loadDraftIntoState } = useBilling();

  useEffect(() => {
    const draft = loadDraft();
    if (draft) {
      loadDraftIntoState(draft);
    }
  }, [loadDraftIntoState]);

  return (
    <div className="app-shell">
      <Header page={page} setPage={setPage} />

      {page === "billing" && (
        <div className="billing-layout">
          <CatalogPanel />
          <InvoicePanel />
        </div>
      )}

      {page === "history" && <History />}

      {page === "reports" && <Reports />}
    </div>
  );
}