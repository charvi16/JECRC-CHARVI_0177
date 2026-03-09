using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public double Salary { get; set; }
}

public class AnalyticsEngine
{
    public void ShowHighSalaryEmployees(List<Employee> employees)
    {
        var highSalary = employees.Where(e => e.Salary >= 50000);

        Console.WriteLine("High Salary Employees:");
        highSalary.ToList().ForEach(e => Console.WriteLine(e.Name));
    }

    public void ShowEmployeesSortedBySalary(List<Employee> employees)
    {
        var sorted = employees.OrderByDescending(e => e.Salary);

        Console.WriteLine("\nEmployees Sorted by Salary:");
        sorted.ToList().ForEach(e => Console.WriteLine($"{e.Name} - {e.Salary}"));
    }

    public void ShowAverageSalary(List<Employee> employees)
    {
        var avg = employees.Average(e => e.Salary);

        Console.WriteLine("\nAverage Salary:");
        Console.WriteLine("Rs " + avg);
    }
}

public class Solution
{
    public static void Main()
    {
        List<Employee> employees = new List<Employee>()
        {
            new Employee { EmployeeId = 301, Name = "Ramesh", Salary = 45000 },
            new Employee { EmployeeId = 302, Name = "Suresh", Salary = 52000 },
            new Employee { EmployeeId = 303, Name = "Kavya", Salary = 68000 },
            new Employee { EmployeeId = 304, Name = "Anita", Salary = 39000 }
        };

        AnalyticsEngine engine = new AnalyticsEngine();

        engine.ShowHighSalaryEmployees(employees);
        engine.ShowEmployeesSortedBySalary(employees);
        engine.ShowAverageSalary(employees);
        Console.WriteLine();
    }
}