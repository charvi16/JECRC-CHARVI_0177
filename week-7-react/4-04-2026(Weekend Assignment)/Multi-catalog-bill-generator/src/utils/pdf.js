import jsPDF from "jspdf";

export function exportInvoicePdf(invoice) {
  const doc = new jsPDF();

  let y = 20;
  doc.setFontSize(18);
  doc.text("Invoice", 14, y);

  y += 10;
  doc.setFontSize(11);
  doc.text(`Invoice No: ${invoice.invoiceNumber}`, 14, y);
  y += 7;
  doc.text(`Date: ${new Date(invoice.createdAt || invoice.invoiceDate).toLocaleString()}`, 14, y);

  y += 12;
  doc.text("Items", 14, y);
  y += 8;

  invoice.items.forEach((item, index) => {
    const line = `${index + 1}. ${item.itemName || item.name} | Qty: ${item.quantity || item.qty} | Price: ₹${item.unitPrice || item.price} | Tax: ${item.taxPercentage ?? item.tax}%`;
    doc.text(line, 14, y);
    y += 7;
  });

  y += 8;
  doc.text(`Subtotal: ₹${Number(invoice.subtotal).toFixed(2)}`, 14, y);
  y += 7;
  doc.text(`Tax: ₹${Number(invoice.taxAmount).toFixed(2)}`, 14, y);
  y += 7;
  doc.text(`Discount: ₹${Number(invoice.discountAmount).toFixed(2)}`, 14, y);
  y += 7;
  doc.text(`Total: ₹${Number(invoice.grandTotal || invoice.total).toFixed(2)}`, 14, y);

  doc.save(`${invoice.invoiceNumber}.pdf`);
}