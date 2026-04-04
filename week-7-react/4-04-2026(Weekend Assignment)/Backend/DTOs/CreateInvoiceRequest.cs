namespace MultiCatalogBillGenerator.DTOs
{
    public class CreateInvoiceRequest
    {
        public string DiscountType { get; set; } = "none";
        public decimal DiscountValue { get; set; }
        public List<CreateInvoiceItemRequest> Items { get; set; } = new();
    }

    public class CreateInvoiceItemRequest
    {
        public string ItemName { get; set; } = "";
        public string CatalogType { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TaxPercentage { get; set; }
        public bool IsCustomItem { get; set; }
    }
}