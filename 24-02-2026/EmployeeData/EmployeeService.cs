using MySql.Data.MySqlClient;
using System;

namespace EmployeeManagementSystem
{
    internal class EmployeeService
    {
        private readonly string connectionString =
        "server=127.0.0.1;port=3307;database=EmployeeDB;user=root;password=MySQLRoot@309";

        EmployeeModel emp = new EmployeeModel();

        public void Run()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== Employee Management System ===");
                Console.WriteLine("1. View All Employees");
                Console.WriteLine("2. Insert Employee");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Search by Employee ID");
                Console.WriteLine("6. Search by Department");
                Console.WriteLine("7. Exit");

                Console.Write("Enter your choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ViewAllEmployees();
                        break;

                    case 2:
                        InsertEmployee();
                        break;

                    case 3:
                        UpdateEmployee();
                        break;

                    case 4:
                        DeleteEmployee();
                        break;

                    case 5:
                        SearchById();
                        break;

                    case 6:
                        SearchByDepartment();
                        break;

                    case 7:
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        // View All Employees
        public void ViewAllEmployees()
        {
            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            string query = "SELECT Id, Name, Department, Salary FROM Employee";

            using MySqlCommand cmd = new MySqlCommand(query, conn);
            using MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== Employee List ===");

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader.GetInt32("Id")} | " +
                    $"Name: {reader.GetString("Name")} | " +
                    $"Department: {reader.GetString("Department")} | " +
                    $"Salary: {reader.GetInt32("Salary")}");
            }
        }

        // Insert Employee
        public void InsertEmployee()
        {
            EmployeeModel emp = new EmployeeModel();

            Console.Write("Enter Name: ");
            emp.Name = Console.ReadLine();

            Console.Write("Enter Department: ");
            emp.Department = Console.ReadLine();

            Console.Write("Enter Salary: ");
            emp.Salary = Convert.ToInt32(Console.ReadLine());

            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            string query =
            "INSERT INTO Employee (Name, Department, Salary) VALUES (@Name,@Department,@Salary)";

            using MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Department", emp.Department);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);

            cmd.ExecuteNonQuery();

            Console.WriteLine("Employee Inserted Successfully!");
        }

        // Delete Employee
        public void DeleteEmployee()
        {
            Console.Write("Enter Employee ID: ");

            int id = Convert.ToInt32(Console.ReadLine());

            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            string query = "DELETE FROM Employee WHERE Id=@Id";

            using MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Id", id);

            int rows = cmd.ExecuteNonQuery();

            Console.WriteLine(rows > 0 ?
                "Employee Deleted Successfully"
                :
                "Employee Not Found");
        }

        // Update Employee
        public void UpdateEmployee()
        {
            Console.Write("Enter Employee ID: ");
            emp.Id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Name: ");
            emp.Name = Console.ReadLine();

            Console.Write("Enter Department: ");
            emp.Department = Console.ReadLine();

            Console.Write("Enter Salary: ");
            emp.Salary = Convert.ToInt32(Console.ReadLine());

            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            string query =
            "UPDATE Employee SET Name=@Name, Department=@Department, Salary=@Salary WHERE Id=@Id";

            using MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Id", emp.Id);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Department", emp.Department);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);

            int rows = cmd.ExecuteNonQuery();

            Console.WriteLine(rows > 0 ?
                "Employee Updated Successfully"
                :
                "Employee Not Found");
        }

        // Search By ID
        public void SearchById()
        {
            Console.Write("Enter Employee ID: ");

            int id = Convert.ToInt32(Console.ReadLine());

            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            string query =
            "SELECT Id, Name, Department, Salary FROM Employee WHERE Id=@Id";

            using MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Id", id);

            using MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader.GetInt32("Id")} | " +
                    $"Name: {reader.GetString("Name")} | " +
                    $"Department: {reader.GetString("Department")} | " +
                    $"Salary: {reader.GetInt32("Salary")}");
            }
            else
            {
                Console.WriteLine("Employee Not Found");
            }
        }

        // Search By Department
        public void SearchByDepartment()
        {
            Console.Write("Enter Department Name: ");

            string dept = Console.ReadLine();

            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            string query =
            "SELECT Id, Name, Department, Salary FROM Employee WHERE Department=@Department";

            using MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Department", dept);

            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader.GetInt32("Id")} | " +
                    $"Name: {reader.GetString("Name")} | " +
                    $"Department: {reader.GetString("Department")} | " +
                    $"Salary: {reader.GetInt32("Salary")}");
            }
        }
    }
}