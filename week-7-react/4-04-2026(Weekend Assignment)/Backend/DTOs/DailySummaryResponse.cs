namespace MultiCatalogBillGenerator.DTOs
{
    public class DailySummaryResponse
    {
        public int TotalBills { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalDiscount { get; set; }
    }
}