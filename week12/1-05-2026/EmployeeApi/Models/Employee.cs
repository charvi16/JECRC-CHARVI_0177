using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApi.Models;

[Table("Employees")] 
public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Department { get; set; }

    public decimal Salary { get; set; }

    public DateTime CreatedDate { get; set; }
}