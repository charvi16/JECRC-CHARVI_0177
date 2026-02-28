using System;

namespace StudentEligibilityValidationSystem
{
    // 1. Entity Class
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Marks { get; set; }
        public int Age { get; set; }
        public int Attendance { get; set; }
    }

    // 3. Eligibility Engine (Core Component)
    public class EligibilityEngine
    {
        public void CheckEligibility(Student student, string program, Predicate<Student> rule)
        {
            bool isEligible = rule(student);

            Console.WriteLine("========= ELIGIBILITY CHECK =========");
            Console.WriteLine($"Student Name : {student.Name}");
            Console.WriteLine($"Program      : {program}");
            Console.WriteLine($"Eligible     : {isEligible}");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 4. Create Student Object (Hardcoded Dataset)
            Student student = new Student
            {
                StudentId = 301,
                Name = "Ananya",
                Marks = 78,
                Age = 18,
                Attendance = 85
            };

            // 2. Define Eligibility Rules using Predicate<Student>

            // Engineering Program → Marks ≥ 60
            Predicate<Student> engineeringEligibility = s => s.Marks >= 60;

            // Medical Program → Marks ≥ 70 AND Age ≥ 17
            Predicate<Student> medicalEligibility = s => s.Marks >= 70 && s.Age >= 17;

            // Management Program → Marks ≥ 55 AND Attendance ≥ 75
            Predicate<Student> managementEligibility = s => s.Marks >= 55 && s.Attendance >= 75;

            // 3. Create Eligibility Engine
            EligibilityEngine engine = new EligibilityEngine();

            // 4. Validate Eligibility for Programs
            engine.CheckEligibility(student, "Engineering", engineeringEligibility);
            engine.CheckEligibility(student, "Medical", medicalEligibility);
            engine.CheckEligibility(student, "Management", managementEligibility);

            Console.ReadLine();
        }
    }
}