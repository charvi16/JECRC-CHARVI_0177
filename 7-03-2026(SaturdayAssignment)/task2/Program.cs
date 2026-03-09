using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentPerformanceAnalysis
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Marks { get; set; }
    }

    public class AnalysisEngine
    {
        private List<Student> students;

        public AnalysisEngine(List<Student> students)
        {
            this.students = students;
        }

        public IEnumerable<Student> GetPassedStudents()
        {
            return students.Where(s => s.Marks >= 50);
        }

        public Student GetTopper()
        {
            int maxMarks = students.Max(s => s.Marks);

            return students
                .Where(s => s.Marks == maxMarks)
                .FirstOrDefault();
        }

        public IEnumerable<Student> GetSortedStudents()
        {
            return students
                .OrderByDescending(s => s.Marks);
        }

        public double GetAverageMarks()
        {
            return students.Average(s => s.Marks);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Student { StudentId = 101, Name = "Ananya", Marks = 78 },
                new Student { StudentId = 102, Name = "Ravi", Marks = 45 },
                new Student { StudentId = 103, Name = "Neha", Marks = 88 },
                new Student { StudentId = 104, Name = "Arjun", Marks = 67 }
            };

            AnalysisEngine engine = new AnalysisEngine(students);

            Console.WriteLine("Passed Students:");

            engine.GetPassedStudents()
                .Select(s => s.Name)
                .ToList()
                .ForEach(name => Console.WriteLine(name));

            var topper = engine.GetTopper();

            Console.WriteLine("\nTopper:");
            Console.WriteLine($"{topper.Name} - {topper.Marks}");

            Console.WriteLine("\nStudents Sorted by Marks:");

            engine.GetSortedStudents()
                .Select(s => $"{s.Name} - {s.Marks}")
                .ToList()
                .ForEach(Console.WriteLine);
            Console.WriteLine();
        }
    }
}