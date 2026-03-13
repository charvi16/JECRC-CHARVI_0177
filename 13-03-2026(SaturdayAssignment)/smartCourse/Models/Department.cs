using System.ComponentModel.DataAnnotations;

namespace SmartCourseApi.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        public ICollection<Course>? Courses { get; set; }
    }
}