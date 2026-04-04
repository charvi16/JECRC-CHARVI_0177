import { useEffect, useMemo, useState, useRef } from "react";
import { useBilling } from "../context/BillingContext";
import { calculateTotals } from "../utils/calculate";
import { clearDraft, saveDraft } from "../utils/storage";
import { exportInvoicePdf } from "../utils/pdf";
import { exportInvoiceCsv } from "../utils/csv";
import DiscountBox from "./DiscountBox";
import InvoiceItemRow from "./InvoiceItemRow";
import SummaryBox from "./SummaryBox";
import { createInvoice } from "../api";
import { useReactToPrint } from "react-to-print";
import PrintInvoice from "./PrintInvoice";

export default function InvoicePanel() {
  const {
    items,
    discount,
    invoiceNumber,
    invoiceDate,
    setInvoiceNumber,
    resetBilling,
  } = useBilling();

  const [saving, setSaving] = useState(false);

  useEffect(() => {
    if (!invoiceNumber) {
      const now = new Date();
      const datePart = now.toISOString().slice(0, 10).replaceAll("-", "");
      setInvoiceNumber(`DRAFT-${datePart}-${Math.floor(1000 + Math.random() * 9000)}`);
    }
  }, [invoiceNumber, setInvoiceNumber]);

  useEffect(() => {
    saveDraft({
      items,
      discount,
      invoiceNumber,
      invoiceDate,
    });
  }, [items, discount, invoiceNumber, invoiceDate]);

  const totals = useMemo(() => calculateTotals(items, discount), [items, discount]);

  async function handleSaveInvoice() {
    if (items.length === 0) {
      alert("Add at least one item.");
      return;
    }

    try {
      setSaving(true);

      const payload = {
        discountType: discount.type,
        discountValue: Number(discount.value),
        items: items.map((item) => ({
          itemName: item.name,
          catalogType: item.catalogType,
          unitPrice: Number(item.price),
          quantity: Number(item.qty),
          taxPercentage: Number(item.tax),
          isCustomItem: item.catalogType === "Custom",
        })),
      };

      const result = await createInvoice(payload);
      clearDraft();
      alert(`Invoice saved: ${result.invoiceNumber}`);
      resetBilling();
    } catch (error) {
      console.error(error);
      alert("Failed to save invoice");
    } finally {
      setSaving(false);
    }
  }

  function buildClientInvoiceForExport() {
    return {
      invoiceNumber,
      invoiceDate,
      items: items.map((item) => ({
        name: item.name,
        catalogType: item.catalogType,
        price: item.price,
        qty: item.qty,
        tax: item.tax,
      })),
      subtotal: totals.subtotal,
      taxAmount: totals.taxAmount,
      discountAmount: totals.discountAmount,
      total: totals.total,
    };
  }
  const printRef = useRef(null);

  const handlePrint = useReactToPrint({
  contentRef: printRef,   
  documentTitle: invoiceNumber,
});

  return (
    <div className="panel">
      <div className="invoice-top">
        <div>
          <h2 style={{ margin: 0 }}>Current Bill</h2>
          <div className="item-meta">Edit quantity, price, tax, and discounts in real time.</div>
        </div>

        <div className="badge">{invoiceNumber || "Draft Invoice"}</div>
      </div>

      <div className="invoice-list">
        {items.length === 0 ? (
          <div className="empty">No items added yet.</div>
        ) : (
          items.map((item) => <InvoiceItemRow key={item.id} item={item} />)
        )}
      </div>

      <div className="summary-wrap">
        <DiscountBox />
        <SummaryBox totals={totals} />

        <div className="actions-box">
          <button className="success-btn" onClick={handleSaveInvoice} disabled={saving}>
            {saving ? "Saving..." : "Finalize & Save"}
          </button>

          <button
            className="secondary-btn"
            onClick={() => exportInvoicePdf(buildClientInvoiceForExport())}
          >
            Export PDF
          </button>

          <button
            className="secondary-btn"
            onClick={() => exportInvoiceCsv(buildClientInvoiceForExport())}
          >
            Export CSV
          </button>

          <button className="secondary-btn" onClick={handlePrint}>
            Print Invoice
          </button>
          <div style={{ position: "absolute", left: "-9999px", top: 0 }}>
            <PrintInvoice
              ref={printRef}
              invoice={{
                invoiceNumber,
                items,
                total: totals.total,
              }}
            />
          </div>

          <button
            className="danger-btn"
            onClick={() => {
              clearDraft();
              resetBilling();
            }}
          >
            Clear Draft
          </button>
        </div>
      </div>
    </div>
  );
}