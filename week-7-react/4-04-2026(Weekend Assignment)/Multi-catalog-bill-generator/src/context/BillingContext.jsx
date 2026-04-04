import { createContext, useCallback, useContext, useMemo, useState } from "react";

const BillingContext = createContext();

export function BillingProvider({ children }) {
  const [items, setItems] = useState([]);
  const [discount, setDiscount] = useState({ type: "none", value: 0 });
  const [invoiceNumber, setInvoiceNumber] = useState("");
  const [invoiceDate, setInvoiceDate] = useState(new Date().toISOString());

  const generateDraftInvoiceNumber = () => {
    const now = new Date();
    const datePart = now.toISOString().slice(0, 10).replaceAll("-", "");
    return `DRAFT-${datePart}-${Math.floor(1000 + Math.random() * 9000)}`;
  };

  const resetBilling = useCallback(() => {
    setItems([]);
    setDiscount({ type: "none", value: 0 });
    setInvoiceNumber(generateDraftInvoiceNumber());
    setInvoiceDate(new Date().toISOString());
  }, []);

  const loadDraftIntoState = useCallback((draft) => {
    setItems(draft.items || []);
    setDiscount(draft.discount || { type: "none", value: 0 });
    setInvoiceNumber(draft.invoiceNumber || generateDraftInvoiceNumber());
    setInvoiceDate(draft.invoiceDate || new Date().toISOString());
  }, []);

  const addItem = (item) => {
    setItems((prev) => {
      const existing = prev.find(
        (x) =>
          x.name === item.name &&
          x.catalogType === item.catalogType &&
          Number(x.price) === Number(item.price)
      );

      if (existing) {
        return prev.map((x) =>
          x.id === existing.id ? { ...x, qty: x.qty + 1 } : x
        );
      }

      return [
        ...prev,
        {
          ...item,
          id: crypto.randomUUID(),
          qty: item.qty || 1,
        },
      ];
    });
  };

  const removeItem = (id) => {
    setItems((prev) => prev.filter((x) => x.id !== id));
  };

  const updateQty = (id, qty) => {
    const safeQty = Math.max(1, Number(qty) || 1);
    setItems((prev) =>
      prev.map((x) => (x.id === id ? { ...x, qty: safeQty } : x))
    );
  };

  const updatePrice = (id, price) => {
    const safePrice = Math.max(0, Number(price) || 0);
    setItems((prev) =>
      prev.map((x) => (x.id === id ? { ...x, price: safePrice } : x))
    );
  };

  const updateTax = (id, tax) => {
    const safeTax = Math.max(0, Number(tax) || 0);
    setItems((prev) =>
      prev.map((x) => (x.id === id ? { ...x, tax: safeTax } : x))
    );
  };

  const value = useMemo(
    () => ({
      items,
      discount,
      invoiceNumber,
      invoiceDate,
      setDiscount,
      setInvoiceNumber,
      setInvoiceDate,
      addItem,
      removeItem,
      updateQty,
      updatePrice,
      updateTax,
      resetBilling,
      loadDraftIntoState,
    }),
    [items, discount, invoiceNumber, invoiceDate, resetBilling, loadDraftIntoState]
  );

  return (
    <BillingContext.Provider value={value}>{children}</BillingContext.Provider>
  );
}

export function useBilling() {
  return useContext(BillingContext);
}