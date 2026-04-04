const BASE_URL = "http://localhost:5053/api";

export async function getCatalogItems() {
  const res = await fetch(`${BASE_URL}/catalog`);
  if (!res.ok) throw new Error("Failed to load catalog");
  return res.json();
}

export async function createCatalogItem(payload) {
  const res = await fetch(`${BASE_URL}/catalog`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });
  if (!res.ok) throw new Error("Failed to create catalog item");
  return res.json();
}

export async function getInvoices() {
  const res = await fetch(`${BASE_URL}/invoice`);
  if (!res.ok) throw new Error("Failed to load invoices");
  return res.json();
}

export async function createInvoice(payload) {
  const res = await fetch(`${BASE_URL}/invoice`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });
  if (!res.ok) throw new Error("Failed to create invoice");
  return res.json();
}

export async function getDailySummary(date) {
  const res = await fetch(`${BASE_URL}/invoice/daily-summary?date=${date}`);
  if (!res.ok) throw new Error("Failed to load summary");
  return res.json();
}