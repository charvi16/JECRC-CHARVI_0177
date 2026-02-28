// using Microsoft.AspNetCore.Mvc;
// using MySqlConnector;
// using EmployeeApi.Models;

// namespace EmployeeApi.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class EmployeesController : ControllerBase
//     {
//         private readonly string _connectionString;

//         public EmployeesController(IConfiguration configuration)
//         {
//             _connectionString = configuration.GetConnectionString("DefaultConnection");
//         }

//         [HttpGet]  // GET all employees
//         public IActionResult GetAll()
//         {
//             List<Employee> employees = new();

//             using var conn = new MySqlConnection(_connectionString);
//             conn.Open();

//             string query = "SELECT * FROM EMP";
//             using var cmd = new MySqlCommand(query, conn);
//             using var reader = cmd.ExecuteReader();

//             while (reader.Read())
//             {
//                 employees.Add(new Employee
//                 {
//                     EmployeeId = reader.GetInt32("employee_id"),
//                     EmployeeName = reader.GetString("employee_name"),
//                     EmployeeSalary = reader.GetDecimal("employee_salary"),
//                     EmployeeDepartment = reader.GetString("employee_department")
//                 });
//             }

//             return Ok(employees);
//         }

//         [HttpPost] // Insert employee
//         public IActionResult Insert(Employee emp)
//         {
//             using var conn = new MySqlConnection(_connectionString);
//             conn.Open();

//             string query = @"INSERT INTO EMP (employee_name, employee_salary, employee_department)
//                              VALUES (@name,@salary,@dept)";

//             using var cmd = new MySqlCommand(query, conn);
//             cmd.Parameters.AddWithValue("@name", emp.EmployeeName);
//             cmd.Parameters.AddWithValue("@salary", emp.EmployeeSalary);
//             cmd.Parameters.AddWithValue("@dept", emp.EmployeeDepartment);

//             cmd.ExecuteNonQuery();

//             return Ok("Employee Inserted");
//         }

//         [HttpPut("{id}")] // Update salary
//         public IActionResult UpdateSalary(int id, Employee emp)
//         {
//             using var conn = new MySqlConnection(_connectionString);
//             conn.Open();

//             string query = "UPDATE EMP SET employee_salary=@salary WHERE employee_id=@id";

//             using var cmd = new MySqlCommand(query, conn);
//             cmd.Parameters.AddWithValue("@salary", emp.EmployeeSalary);
//             cmd.Parameters.AddWithValue("@id", id);

//             cmd.ExecuteNonQuery();

//             return Ok("Salary Updated");
//         }

//         [HttpDelete("{id}")] // Delete employee
//         public IActionResult Delete(int id)
//         {
//             using var conn = new MySqlConnection(_connectionString);
//             conn.Open();

//             string query = "DELETE FROM EMP WHERE employee_id=@id";

//             using var cmd = new MySqlCommand(query, conn);
//             cmd.Parameters.AddWithValue("@id", id);

//             cmd.ExecuteNonQuery();

//             return Ok("Employee Deleted");
//         }

//         [HttpGet("department/{dept}")] // Search by department
//         public IActionResult SearchByDepartment(string dept)
//         {
//             List<Employee> employees = new();

//             using var conn = new MySqlConnection(_connectionString);
//             conn.Open();

//             string query = "SELECT * FROM EMP WHERE employee_department=@dept";

//             using var cmd = new MySqlCommand(query, conn);
//             cmd.Parameters.AddWithValue("@dept", dept);

//             using var reader = cmd.ExecuteReader();

//             while (reader.Read())
//             {
//                 employees.Add(new Employee
//                 {
//                     EmployeeId = reader.GetInt32("employee_id"),
//                     EmployeeName = reader.GetString("employee_name"),
//                     EmployeeSalary = reader.GetDecimal("employee_salary"),
//                     EmployeeDepartment = reader.GetString("employee_department")
//                 });
//             }

//             return Ok(employees);
//         }
//     }
// }



using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Collections.Generic;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {

        string connectionString =
        "server=127.0.0.1;port=3307;database=testdb;user=root;password=MySQLRoot@309;";



        // ✅ 1. GET ALL EMPLOYEES
        [HttpGet]
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM EMP";

            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                employees.Add(new Employee
                {
                    EmployeeId = reader.GetInt32("employee_id"),
                    EmployeeName = reader.GetString("employee_name"),
                    EmployeeSalary = reader.GetDecimal("employee_salary"),
                    EmployeeDepartment = reader.GetString("employee_department")
                });
            }

            return employees;
        }



        // ✅ 2. INSERT EMPLOYEE
        [HttpPost]
        public string InsertEmployee(Employee emp)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "INSERT INTO EMP(employee_name,employee_salary,employee_department) VALUES(@name,@salary,@dept)";

            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@name", emp.EmployeeName);
            command.Parameters.AddWithValue("@salary", emp.EmployeeSalary);
            command.Parameters.AddWithValue("@dept", emp.EmployeeDepartment);

            command.ExecuteNonQuery();

            return "Employee Inserted Successfully";
        }



        // ✅ 3. UPDATE SALARY
        [HttpPut("{id}")]
        public string UpdateSalary(int id, decimal salary)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "UPDATE EMP SET employee_salary=@salary WHERE employee_id=@id";

            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@salary", salary);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();

            return "Salary Updated Successfully";
        }



        // ✅ 4. DELETE EMPLOYEE
        [HttpDelete("{id}")]
        public string DeleteEmployee(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "DELETE FROM EMP WHERE employee_id=@id";

            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();

            return "Employee Deleted Successfully";
        }



        // ✅ 5. SEARCH BY DEPARTMENT
        [HttpGet("department/{dept}")]
        public List<Employee> SearchDepartment(string dept)
        {
            List<Employee> employees = new List<Employee>();

            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM EMP WHERE employee_department=@dept";

            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@dept", dept);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                employees.Add(new Employee
                {
                    EmployeeId = reader.GetInt32("employee_id"),
                    EmployeeName = reader.GetString("employee_name"),
                    EmployeeSalary = reader.GetDecimal("employee_salary"),
                    EmployeeDepartment = reader.GetString("employee_department")
                });
            }

            return employees;
        }

    }



    // Employee Model
    public class Employee
    {
        public int? EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        public decimal? EmployeeSalary { get; set; }

        public string? EmployeeDepartment { get; set; }
    }

}