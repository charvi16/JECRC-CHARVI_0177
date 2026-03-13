using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCourseApi.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [MaxLength(150)]
        public string ? CourseName { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        [Required]
        public int Credits { get; set; }

        [Required]
        public int SeatsAvailable { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}