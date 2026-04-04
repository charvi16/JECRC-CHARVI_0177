namespace MultiCatalogBillGenerator.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        // public Invoice? Invoice { get; set; }

        public string ItemName { get; set; } = "";
        public string CatalogType { get; set; } = "";

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal LineTotal { get; set; }
        public bool IsCustomItem { get; set; }
    }
}