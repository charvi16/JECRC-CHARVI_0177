import { useEffect, useMemo, useState } from "react";
import { useBilling } from "../context/BillingContext";
import { createCatalogItem, getCatalogItems } from "../api";

const tabs = ["Entrance", "Donation", "SellingPrice", "Custom"];

export default function CatalogPanel() {
  const [activeTab, setActiveTab] = useState("Entrance");
  const [catalogItems, setCatalogItems] = useState([]);
  const [loading, setLoading] = useState(true);
  const { addItem } = useBilling();

  const [customItem, setCustomItem] = useState({
    name: "",
    price: "",
    tax: 0,
    qty: 1,
  });

  const [newCatalogItem, setNewCatalogItem] = useState({
    name: "",
    catalogType: "SellingPrice",
    defaultPrice: "",
    taxPercentage: 5,
    isCustomPriceAllowed: false,
  });

  useEffect(() => {
    loadCatalog();
  }, []);

  async function loadCatalog() {
    try {
      setLoading(true);
      const data = await getCatalogItems();
      setCatalogItems(data);
    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  }

  const filteredItems = useMemo(
    () => catalogItems.filter((item) => item.catalogType === activeTab),
    [catalogItems, activeTab]
  );

  function handleAddCatalogItem(item) {
    addItem({
      name: item.name,
      catalogType: item.catalogType,
      price: item.defaultPrice,
      tax: item.taxPercentage,
      qty: 1,
    });
  }

  function handleAddCustomItem() {
    if (!customItem.name || !customItem.price) return;

    addItem({
      name: customItem.name,
      catalogType: "Custom",
      price: Number(customItem.price),
      tax: Number(customItem.tax),
      qty: Number(customItem.qty),
    });

    setCustomItem({
      name: "",
      price: "",
      tax: 0,
      qty: 1,
    });
  }

  async function handleCreateCatalogItem(e) {
    e.preventDefault();
    if (!newCatalogItem.name || !newCatalogItem.defaultPrice) return;

    try {
      await createCatalogItem({
        ...newCatalogItem,
        defaultPrice: Number(newCatalogItem.defaultPrice),
        taxPercentage: Number(newCatalogItem.taxPercentage),
      });

      setNewCatalogItem({
        name: "",
        catalogType: "SellingPrice",
        defaultPrice: "",
        taxPercentage: 5,
        isCustomPriceAllowed: false,
      });

      loadCatalog();
    } catch (error) {
      console.error(error);
      alert("Failed to create catalog item");
    }
  }

  return (
    <div className="panel">
      <div className="panel-title-row">
        <h2>Catalogs</h2>
        <span className="badge">{activeTab}</span>
      </div>

      <div className="tabs">
        {tabs.map((tab) => (
          <button
            key={tab}
            className={`tab ${activeTab === tab ? "active" : ""}`}
            onClick={() => setActiveTab(tab)}
          >
            {tab}
          </button>
        ))}
      </div>

      {activeTab !== "Custom" && (
        <>
          {loading ? (
            <div className="empty">Loading catalog...</div>
          ) : filteredItems.length === 0 ? (
            <div className="empty">No items found in this catalog.</div>
          ) : (
            <div className="grid-items">
              {filteredItems.map((item) => (
                <div className="item-card" key={item.id}>
                  <h4>{item.name}</h4>
                  <div className="item-meta">
                    ₹{item.defaultPrice} · Tax {item.taxPercentage}%
                  </div>
                  <button
                    className="primary-btn"
                    onClick={() => handleAddCatalogItem(item)}
                  >
                    Add to Bill
                  </button>
                </div>
              ))}
            </div>
          )}
        </>
      )}

      {activeTab === "Custom" && (
        <div className="custom-form">
          <input
            placeholder="Custom item name"
            value={customItem.name}
            onChange={(e) =>
              setCustomItem((prev) => ({ ...prev, name: e.target.value }))
            }
          />
          <input
            type="number"
            placeholder="Price"
            value={customItem.price}
            onChange={(e) =>
              setCustomItem((prev) => ({ ...prev, price: e.target.value }))
            }
          />
          <input
            type="number"
            placeholder="Tax %"
            value={customItem.tax}
            onChange={(e) =>
              setCustomItem((prev) => ({ ...prev, tax: e.target.value }))
            }
          />
          <input
            type="number"
            placeholder="Quantity"
            value={customItem.qty}
            onChange={(e) =>
              setCustomItem((prev) => ({ ...prev, qty: e.target.value }))
            }
          />
          <button className="success-btn full" onClick={handleAddCustomItem}>
            Add Custom Item
          </button>
        </div>
      )}

      <div style={{ marginTop: 18 }}>
        <div className="panel-title-row">
          <h3>Add New Catalog Product</h3>
        </div>

        <form className="custom-form" onSubmit={handleCreateCatalogItem}>
          <input
            placeholder="Item name"
            value={newCatalogItem.name}
            onChange={(e) =>
              setNewCatalogItem((prev) => ({ ...prev, name: e.target.value }))
            }
          />
          <select
            value={newCatalogItem.catalogType}
            onChange={(e) =>
              setNewCatalogItem((prev) => ({
                ...prev,
                catalogType: e.target.value,
              }))
            }
          >
            <option value="Entrance">Entrance</option>
            <option value="Donation">Donation</option>
            <option value="SellingPrice">SellingPrice</option>
          </select>

          <input
            type="number"
            placeholder="Default price"
            value={newCatalogItem.defaultPrice}
            onChange={(e) =>
              setNewCatalogItem((prev) => ({
                ...prev,
                defaultPrice: e.target.value,
              }))
            }
          />
          <input
            type="number"
            placeholder="Tax %"
            value={newCatalogItem.taxPercentage}
            onChange={(e) =>
              setNewCatalogItem((prev) => ({
                ...prev,
                taxPercentage: e.target.value,
              }))
            }
          />

          <button className="primary-btn full" type="submit">
            Save Catalog Item
          </button>
        </form>
      </div>
    </div>
  );
}