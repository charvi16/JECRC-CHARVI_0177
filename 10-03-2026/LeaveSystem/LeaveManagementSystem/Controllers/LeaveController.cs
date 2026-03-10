using System.Security.Claims;
using LeaveManagementAPI.Data;
using LeaveManagementAPI.DTOs;
using LeaveManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeaveController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("apply")]
        public IActionResult ApplyLeave(LeaveRequestDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var leave = new LeaveRequest
            {
                EmployeeId = userId,
                LeaveType = dto.LeaveType,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Reason = dto.Reason,
                Status = "Pending"
            };

            _context.LeaveRequests.Add(leave);
            _context.SaveChanges();

            return Ok("Leave applied successfully");
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("my-leaves")]
        public IActionResult MyLeaves()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var leaves = _context.LeaveRequests
                .Where(l => l.EmployeeId == userId)
                .ToList();

            return Ok(leaves);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("all")]
        public IActionResult AllLeaves()
        {
            var leaves = _context.LeaveRequests
                .Include(l => l.Employee)
                .Select(l => new
                {
                    l.Id,
                    EmployeeName = l.Employee!.Username,
                    l.LeaveType,
                    l.StartDate,
                    l.EndDate,
                    l.Reason,
                    l.Status
                })
                .ToList();

            return Ok(leaves);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpPut("approve/{id}")]
        public IActionResult ApproveLeave(int id)
        {
            var leave = _context.LeaveRequests.FirstOrDefault(l => l.Id == id);

            if (leave == null) return NotFound("Leave request not found");

            leave.Status = "Approved";
            _context.SaveChanges();

            return Ok("Leave approved");
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpPut("reject/{id}")]
        public IActionResult RejectLeave(int id)
        {
            var leave = _context.LeaveRequests.FirstOrDefault(l => l.Id == id);

            if (leave == null) return NotFound("Leave request not found");

            leave.Status = "Rejected";
            _context.SaveChanges();

            return Ok("Leave rejected");
        }
    }
}