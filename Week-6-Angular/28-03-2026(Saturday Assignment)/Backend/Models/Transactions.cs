namespace Backend.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ? Description { get; set; }
        public int Type { get; set; } 
        public float Amount { get; set; }
        public string ? Balance { get; set; }
    }
}