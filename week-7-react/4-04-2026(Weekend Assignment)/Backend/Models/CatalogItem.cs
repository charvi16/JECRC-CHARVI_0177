namespace MultiCatalogBillGenerator.Models
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string CatalogType { get; set; } = "";
        public decimal DefaultPrice { get; set; }
        public decimal TaxPercentage { get; set; }
        public bool IsCustomPriceAllowed { get; set; }
        public bool IsActive { get; set; } = true;
    }
}