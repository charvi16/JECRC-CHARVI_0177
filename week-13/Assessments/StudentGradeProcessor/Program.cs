using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string Name { get; private set; }

    public int[] Grades { get; private set; }

    public Student(string name, int[] grades)
    {
        Name = name;
        Grades = grades;
    }

    public double GetAverage()
    {
        return Grades.Average();
    }

    public int GetHighest()
    {
        return Grades.Max();
    }

    public int GetLowest()
    {
        return Grades.Min();
    }

    public bool HasAllGradesAboveOrEqual(int threshold)
    {
        return Grades.All(g => g >= threshold);
    }

    public override string ToString()
    {
        return $"{Name} ({string.Join(",", Grades)})";
    }
}

class GradeProcessor
{
    private List<Student> students;

    public GradeProcessor(List<Student> students)
    {
        this.students = students;
    }

    // 1. Student Report
    public void DisplayStudentReport()
    {
        Console.WriteLine(
            "--- Student Grade Report ---\n"
        );

        foreach (var student in students)
        {
            Console.WriteLine(
                $"{student.Name}: " +
                $"Average = {student.GetAverage():F2}, " +
                $"Highest = {student.GetHighest()}, " +
                $"Lowest = {student.GetLowest()}"
            );
        }

        Console.WriteLine();
    }

    // 2. Top Performer
    public void DisplayTopPerformer()
    {
        var topStudent = students
            .OrderByDescending(s => s.GetAverage())
            .First();

        Console.WriteLine(
            $"Top Performer: {topStudent.Name} " +
            $"(Average: {topStudent.GetAverage():F2})\n"
        );
    }

    // 3. Students Above Threshold
    public void DisplayStudentsAboveThreshold(int threshold)
    {
        Console.WriteLine(
            $"Students with all grades >= {threshold}:\n"
        );

        var filteredStudents = students
            .Where(s => s.HasAllGradesAboveOrEqual(threshold));

        foreach (var student in filteredStudents)
        {
            Console.WriteLine(student);
        }

        Console.WriteLine();
    }

    // 4. Unique Grades
    public void DisplayUniqueGrades()
    {
        HashSet<int> uniqueGrades = new HashSet<int>();

        foreach (var student in students)
        {
            foreach (var grade in student.Grades)
            {
                uniqueGrades.Add(grade);
            }
        }

        var sortedGrades = uniqueGrades.OrderBy(g => g);

        Console.WriteLine(
            "Unique Grade Values Across All Students:\n"
        );

        Console.WriteLine(
            string.Join(",", sortedGrades)
        );

        Console.WriteLine(
            $"Total unique grades: {uniqueGrades.Count}"
        );
    }
}

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        List<Student> students = new List<Student>();

        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries
            );

            string name = input[0];

            int[] grades = input
                .Skip(1)
                .Select(int.Parse)
                .ToArray();

            students.Add(
                new Student(name, grades)
            );
        }

        GradeProcessor processor =
            new GradeProcessor(students);

        processor.DisplayStudentReport();

        processor.DisplayTopPerformer();

        processor.DisplayStudentsAboveThreshold(80);

        processor.DisplayUniqueGrades();
    }
}