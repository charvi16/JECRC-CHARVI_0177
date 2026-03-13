using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCourseApi.Data;
using SmartCourseApi.DTOs;
using SmartCourseApi.Models;
using System.Security.Claims;

namespace SmartCourseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // FR6 - Enroll in course
        [HttpPost("enroll")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Enroll(EnrollmentDto dto)
        {
            var studentIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (studentIdClaim == null)
                return Unauthorized();

            int studentId = int.Parse(studentIdClaim);

            var course = await _context.Courses.FindAsync(dto.CourseId);
            if (course == null)
                return NotFound(new { message = "Course not found" });

            if (course.SeatsAvailable <= 0)
                return BadRequest(new { message = "No seats available" });

            var exists = await _context.Enrollments
                .AnyAsync(e => e.CourseId == dto.CourseId && e.StudentId == studentId && e.DropDate == null);

            if (exists)
                return BadRequest(new { message = "Already enrolled in this course" });

            var enrollment = new Enrollment
            {
                CourseId = dto.CourseId,
                StudentId = studentId,
                EnrollmentDate = DateTime.UtcNow
            };

            course.SeatsAvailable -= 1;
            _context.Enrollments.Add(enrollment);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Enrolled successfully" });
        }

        // FR7 - Drop course
        [HttpPost("drop/{courseId}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Drop(int courseId)
        {
            var studentIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (studentIdClaim == null)
                return Unauthorized();

            int studentId = int.Parse(studentIdClaim);

            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.CourseId == courseId && e.StudentId == studentId && e.DropDate == null);

            if (enrollment == null)
                return NotFound(new { message = "Active enrollment not found" });

            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
                return NotFound(new { message = "Course not found" });

            enrollment.DropDate = DateTime.UtcNow;
            course.SeatsAvailable += 1;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Course dropped successfully" });
        }

        // Student - View my enrollments
        [HttpGet("my")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyEnrollments()
        {
            var studentIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (studentIdClaim == null)
                return Unauthorized();

            int studentId = int.Parse(studentIdClaim);

            var data = await _context.Enrollments
                .Include(e => e.Course)
                .ThenInclude(c => c!.Department)
                .Where(e => e.StudentId == studentId)
                .Select(e => new
                {
                    e.EnrollmentId,
                    CourseId = e.CourseId,
                    CourseName = e.Course!.CourseName,
                    Department = e.Course.Department!.DepartmentName,
                    e.EnrollmentDate,
                    e.DropDate
                })
                .ToListAsync();

            return Ok(data);
        }

        // FR8 - Admin view enrollment history
        [HttpGet("history")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EnrollmentHistory()
        {
            var data = await _context.Enrollments
                .Include(e => e.Course)
                .ThenInclude(c => c!.Department)
                .Include(e => e.Student)
                .Select(e => new
                {
                    e.EnrollmentId,
                    e.CourseId,
                    CourseName = e.Course!.CourseName,
                    Department = e.Course.Department!.DepartmentName,
                    e.StudentId,
                    StudentName = e.Student!.Name,
                    StudentEmail = e.Student.Email,
                    e.EnrollmentDate,
                    e.DropDate
                })
                .ToListAsync();

            return Ok(data);
        }
    }
}