using MySql.Data.MySqlClient;
using EmployeeAPI.Models;

namespace EmployeeAPI.Services
{
    public class EmployeeService
    {
        string connectionString =
        "server=127.0.0.1;port=3307;database=EmployeeDBase;user=root;password=MySQLRoot@309";

        public List<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();

            using MySqlConnection conn =
            new MySqlConnection(connectionString);

            conn.Open();

            string query="SELECT * FROM Employee";

            MySqlCommand cmd=
            new MySqlCommand(query,conn);

            MySqlDataReader reader=
            cmd.ExecuteReader();

            while(reader.Read())
            {
                list.Add(new Employee()
                {
                    Id=reader.GetInt32("Id"),
                    Name=reader.GetString("Name"),
                    Department=reader.GetString("Department"),
                    Salary=reader.GetInt32("Salary")
                });
            }

            return list;
        }


        public void AddEmployee(Employee emp)
        {
            using MySqlConnection conn=
            new MySqlConnection(connectionString);

            conn.Open();

            string query=
            "INSERT INTO Employee(Name,Department,Salary) VALUES(@Name,@Department,@Salary)";

            MySqlCommand cmd=
            new MySqlCommand(query,conn);

            cmd.Parameters.AddWithValue("@Name",emp.Name);
            cmd.Parameters.AddWithValue("@Department",emp.Department);
            cmd.Parameters.AddWithValue("@Salary",emp.Salary);

            cmd.ExecuteNonQuery();
        }
    }
}