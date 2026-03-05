// using System;
// using MySqlConnector;

// namespace EmployeeApp
// {
//     // Employee Class
//     public class Employee
//     {
//         public int ? EmployeeId { get; set; }
//         public string ? EmployeeName { get; set; }
//         public decimal ? EmployeeSalary { get; set; }
//         public string ? EmployeeDepartment { get; set; }
//     }

//     class Program
//     {
//         static void Main(string[] args)
//         {
//             // 🔹 Connection String (Update password if needed)
//             string connectionString = "server=127.0.0.1;port=3307;database=testdb;user=root;password=MySQLRoot@309;";

//             using (var connection = new MySqlConnection(connectionString))
//             {
//                 try
//                 {
//                     connection.Open();
//                     Console.WriteLine("Connected to MySQL successfully!");

//                     string query = "SELECT employee_id, employee_name, employee_salary, employee_department FROM EMP";

//                     using (var command = new MySqlCommand(query, connection))
//                     using (var reader = command.ExecuteReader())
//                     {
//                         while (reader.Read())
//                         {
//                             Employee emp = new Employee
//                             {
//                                 EmployeeId = reader.GetInt32("employee_id"),
//                                 EmployeeName = reader.GetString("employee_name"),
//                                 EmployeeSalary = reader.GetDecimal("employee_salary"),
//                                 EmployeeDepartment = reader.GetString("employee_department")
//                             };

//                             Console.WriteLine("-----------------------------------");
//                             Console.WriteLine($"ID: {emp.EmployeeId}");
//                             Console.WriteLine($"Name: {emp.EmployeeName}");
//                             Console.WriteLine($"Salary: {emp.EmployeeSalary}");
//                             Console.WriteLine($"Department: {emp.EmployeeDepartment}");
//                         }
//                     }
//                 }
//                 catch (Exception ex)
//                 {
//                     Console.WriteLine("Error: " + ex.Message);
//                 }
//             }

//             Console.ReadLine();
//         }
//     }
// }




using System;
using MySqlConnector;

namespace EmployeeApp
{
    public class Employee
    {
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public decimal? EmployeeSalary { get; set; }
        public string? EmployeeDepartment { get; set; }
    }

    class Program
    {
        static string connectionString =
        "server=127.0.0.1;port=3307;database=testdb;user=root;password=MySQLRoot@309;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n===== Employee Management =====");
                Console.WriteLine("1. Insert Employee");
                Console.WriteLine("2. Update Salary");
                Console.WriteLine("3. Delete Employee");
                Console.WriteLine("4. Search by Department");
                Console.WriteLine("5. Show All Employees");
                Console.WriteLine("6. Exit");
                Console.Write("Choose option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1: InsertEmployee(); break;
                    case 2: UpdateSalary(); break;
                    case 3: DeleteEmployee(); break;
                    case 4: SearchByDepartment(); break;
                    case 5: ShowAllEmployees(); break;
                    case 6: return;
                }
            }
        }

        static void InsertEmployee()
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Salary: ");
            decimal salary = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter Department: ");
            string dept = Console.ReadLine();

            string query = "INSERT INTO EMP (employee_name, employee_salary, employee_department) VALUES (@name,@salary,@dept)";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@salary", salary);
            cmd.Parameters.AddWithValue("@dept", dept);

            cmd.ExecuteNonQuery();
            Console.WriteLine("✅ Employee Inserted Successfully!");
        }

        static void UpdateSalary()
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            Console.Write("Enter Employee ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter New Salary: ");
            decimal salary = Convert.ToDecimal(Console.ReadLine());

            string query = "UPDATE EMP SET employee_salary=@salary WHERE employee_id=@id";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@salary", salary);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            Console.WriteLine("✅ Salary Updated Successfully!");
        }

        static void DeleteEmployee()
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            Console.Write("Enter Employee ID to Delete: ");
            int id = Convert.ToInt32(Console.ReadLine());

            string query = "DELETE FROM EMP WHERE employee_id=@id";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            Console.WriteLine("✅ Employee Deleted Successfully!");
        }

        static void SearchByDepartment()
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            Console.Write("Enter Department: ");
            string dept = Console.ReadLine();

            string query = "SELECT * FROM EMP WHERE employee_department=@dept";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@dept", dept);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"ID: {reader["employee_id"]}");
                Console.WriteLine($"Name: {reader["employee_name"]}");
                Console.WriteLine($"Salary: {reader["employee_salary"]}");
                Console.WriteLine($"Department: {reader["employee_department"]}");
            }
        }

        static void ShowAllEmployees()
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            string query = "SELECT * FROM EMP";

            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"ID: {reader["employee_id"]}");
                Console.WriteLine($"Name: {reader["employee_name"]}");
                Console.WriteLine($"Salary: {reader["employee_salary"]}");
                Console.WriteLine($"Department: {reader["employee_department"]}");
            }
        }
    }
}