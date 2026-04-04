export function calculateTotals(items, discount) {
  let subtotal = 0;
  let taxAmount = 0;

  for (const item of items) {
    const lineSubtotal = Number(item.price) * Number(item.qty);
    const lineTax = lineSubtotal * (Number(item.tax) / 100);
    subtotal += lineSubtotal;
    taxAmount += lineTax;
  }

  let discountAmount = 0;

  if (discount.type === "percent") {
    discountAmount = subtotal * (Number(discount.value) / 100);
  } else if (discount.type === "fixed") {
    discountAmount = Number(discount.value);
  }

  if (discountAmount > subtotal + taxAmount) {
    discountAmount = subtotal + taxAmount;
  }

  return {
    subtotal,
    taxAmount,
    discountAmount,
    total: subtotal + taxAmount - discountAmount,
  };
}