namespace MultiCatalogBillGenerator.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "Finalized";

        public string DiscountType { get; set; } = "none";
        public decimal DiscountValue { get; set; }
        public decimal DiscountAmount { get; set; }

        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal GrandTotal { get; set; }

        public List<InvoiceItem> Items { get; set; } = new();
    }
}