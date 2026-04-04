export function exportInvoiceCsv(invoice) {
  const rows = [
    ["Invoice Number", invoice.invoiceNumber],
    ["Date", new Date(invoice.createdAt || invoice.invoiceDate).toLocaleString()],
    [],
    ["Item", "Catalog", "Qty", "Unit Price", "Tax %", "Line Total"],
  ];

  (invoice.items || []).forEach((item) => {
    const qty = item.quantity ?? item.qty;
    const unitPrice = item.unitPrice ?? item.price;
    const tax = item.taxPercentage ?? item.tax;
    const lineTotal = (unitPrice * qty) + ((unitPrice * qty * tax) / 100);

    rows.push([
      item.itemName || item.name,
      item.catalogType,
      qty,
      unitPrice,
      tax,
      lineTotal.toFixed(2),
    ]);
  });

  rows.push([]);
  rows.push(["Subtotal", invoice.subtotal]);
  rows.push(["Tax", invoice.taxAmount]);
  rows.push(["Discount", invoice.discountAmount]);
  rows.push(["Total", invoice.grandTotal || invoice.total]);

  const csvContent = rows
    .map((row) => row.map((cell) => `"${cell ?? ""}"`).join(","))
    .join("\n");

  const blob = new Blob([csvContent], { type: "text/csv;charset=utf-8;" });
  const url = URL.createObjectURL(blob);
  const a = document.createElement("a");
  a.href = url;
  a.download = `${invoice.invoiceNumber}.csv`;
  a.click();
  URL.revokeObjectURL(url);
}