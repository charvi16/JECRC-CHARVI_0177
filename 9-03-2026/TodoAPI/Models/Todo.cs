namespace TodoAPI.Models
{
    public class Todo
    {
        public int Id {get; set;}
        public string ? Title {get; set;}
        public bool IsCompleted {get; set;}
        public string Priority { get; set; } = "Medium";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
