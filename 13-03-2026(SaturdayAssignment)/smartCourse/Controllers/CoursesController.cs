using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCourseApi.Data;
using SmartCourseApi.DTOs;
using SmartCourseApi.Models;

namespace SmartCourseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // FR1 - View Courses
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _context.Courses
                .Include(c => c.Department)
                .Select(c => new
                {
                    c.CourseId,
                    c.CourseName,
                    Department = c.Department!.DepartmentName,
                    c.Credits,
                    c.SeatsAvailable
                })
                .ToListAsync();

            return Ok(courses);
        }

        // FR2 - Search Courses
        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchCourses([FromQuery] string keyword)
        {
            keyword = keyword.ToLower();

            var courses = await _context.Courses
                .Include(c => c.Department)
                .Where(c =>
                    c.CourseName.ToLower().Contains(keyword) ||
                    c.Department!.DepartmentName.ToLower().Contains(keyword))
                .Select(c => new
                {
                    c.CourseId,
                    c.CourseName,
                    Department = c.Department!.DepartmentName,
                    c.Credits,
                    c.SeatsAvailable
                })
                .ToListAsync();

            return Ok(courses);
        }

        // FR3 - Add New Course (Admin)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCourse(CourseDto dto)
        {
            var department = await _context.Departments.FindAsync(dto.DepartmentId);
            if (department == null)
                return BadRequest(new { message = "Invalid DepartmentId" });

            var course = new Course
            {
                CourseName = dto.CourseName,
                DepartmentId = dto.DepartmentId,
                Credits = dto.Credits,
                SeatsAvailable = dto.SeatsAvailable
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Course added successfully" });
        }

        // FR4 - Update Course Details
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCourse(int id, CourseDto dto)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound(new { message = "Course not found" });

            course.CourseName = dto.CourseName;
            course.DepartmentId = dto.DepartmentId;
            course.Credits = dto.Credits;
            course.SeatsAvailable = dto.SeatsAvailable;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Course updated successfully" });
        }

        // FR5 - Delete Course
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound(new { message = "Course not found" });

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Course deleted successfully" });
        }
    }
}