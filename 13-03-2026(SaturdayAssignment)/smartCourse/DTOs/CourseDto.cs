namespace SmartCourseApi.DTOs
{
    public class CourseDto
    {
        public string CourseName { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public int Credits { get; set; }
        public int SeatsAvailable { get; set; }
    }
}