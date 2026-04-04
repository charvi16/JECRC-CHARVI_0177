const DRAFT_KEY = "multi_catalog_bill_draft";

export function saveDraft(data) {
  localStorage.setItem(DRAFT_KEY, JSON.stringify(data));
}

export function loadDraft() {
  const raw = localStorage.getItem(DRAFT_KEY);
  return raw ? JSON.parse(raw) : null;
}

export function clearDraft() {
  localStorage.removeItem(DRAFT_KEY);
}