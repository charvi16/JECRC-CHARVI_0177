// using System;
// namespace AcessModifier
// {
//     class usingaccessModifier{
//         public void PublicMethod()
//         {
//             Console.WriteLine("It's Public");
//         }

//         private void PrivateMethod()
//         {
//             Console.WriteLine("It's Private");
//         }

//         protected void ProtectedMethod()
//         {
//             Console.WriteLine("It's Protected");
//         }

//         protected internal void ProtectedInternalMethod()
//         {
//             Console.WriteLine("It's Protected Internal");
//         }

//         internal void InternalMethod()
//         {   
//             Console.WriteLine("It's Internal");
//         }

//         void someMethod()
//         {
//             Console.WriteLine("something");
//         }
//     }

//     class Program
//     {
//         static void Main(String[] args)
//         {
//             usingaccessModifier  obj  = new usingaccessModifier();
//             obj.PublicMethod();
//             obj.InternalMethod();
//             obj.ProtectedInternalMethod();
//             // obj.ProtectedMethod();
//             // obj.PrivateMethod();
//             // obj.someMethod();
//         }
//     }
// }

// using System;
// namespace AcessModifier
// {
//     public class Person
//     {
//         private string name;
//         private int age;
//         public void GetInformation()
//         {
//             Console.WriteLine("Enter your name");
//             name = Console.ReadLine();
//             age = int.Parse(Console.ReadLine());
//         }
//         public void Display()
//         {
//             Console.WriteLine("Welcome to Training Mr/Mrs {0} , and your age is {1}", name, age);
//         }
//     }
//     public class Employee:Person
//     {
//         private string CompanyName;
//         public int EmployeeID;
//         private int EmployeeScore;
//         public int GetEmployeeInformation()
//         {
//             Console.WriteLine("Enter your Companyname");
//             CompanyName = Console.ReadLine();
//             EmployeeID = int.Parse(Console.ReadLine());
//             EmployeeScore = int.Parse(Console.ReadLine());
//             return EmployeeScore;
//         }

//         public void DisplayEmployeeInfo()
//         {
//             Console.WriteLine("Welcome to Company {0}, and your Id is {1}, and your score is {2}", CompanyName, EmployeeID, EmployeeScore);
//         }
//     }

//     interface IDepartment
//     {
//         string DepartmentName {get; set; }
//         void DisplayDepartmentDetails();
//     }
//     public class GradeLevel : Employee, IDepartment
//     {
//         public string DepartmentName
//         {
//             get; set;
//         }
//         public void CheckEligible()
//         {
//             Console.WriteLine("Every Employee should have above 150");
//             if(this.GetEmployeeInformation()>150)
//             {
//                 Console.WriteLine("You are eligible");
//             }
//             else
//             {
//                 Console.WriteLine("You are not eligible");
//             }
//         }
//         public void DisplayDepartmentDetails()
//         {
//             Console.WriteLine("Department Name: {0}", DepartmentName);
//         }
//     }
//     public class TestProgram
//     {
//         static void Main(String[] args)
//         {
//             GradeLevel obj= new GradeLevel();
//             obj.GetInformation();
//             obj.Display();
//             obj.GetEmployeeInformation();
//             obj.DisplayEmployeeInfo();
//             obj.DepartmentName = "IT";
//             obj.CheckEligible();
//             obj.DisplayDepartmentDetails();
//         }
//     }
// }



// using System;
// // using System.Globalization;

// namespace ConsoleApp
// {
//     class Car
//     {
//         public string Name { get; set; }
//         public int NumberOfDoors { get; set; }
//         public Car()
//         {
//             Name = "NoName";
//             NumberOfDoors = 3;
//         }
//         public Car(string name, int numberOfDoors)
//         {
//             Name = name;
//             NumberOfDoors = numberOfDoors;
//         }
//         public Car(string name)
//         {
//             Name = name;
//             NumberOfDoors = 0;
//         }
//         public Car(int numberOfDoors)
//         {
//             Name = "";
//             NumberOfDoors = numberOfDoors;
//         }
//     }

//     class OLDExercise
//     {
//         static void Main(string[] args)
//         {
//             Car c1 = new Car();
//             Car c2 = new Car(3);
//             Car c3 = new Car("MyName");
//             Car c4 = new Car("MyName", 4);
//             Console.WriteLine(c1.NumberOfDoors);
//             Console.WriteLine(c1.Name);
//         }
//     }
// }


// using System;
// using System.Text.RegularExpressions;
// // using System.Globalization;

// namespace ConsoleApp
// {
//     class GroupAgent
//     {
//         public void show()
//         {
//             Console.WriteLine("GroupAgent Created !!!");
//             Console.ReadLine();
//         }
        
//     }

//     class Agent : GroupAgent
//     {
//         public new void show()
//         {
//             Console.WriteLine("Agent Created !!!");
//             Console.ReadLine();
//         }
        
//     }

//     class OLDExercise
//     {
//         static void Main(string[] args)
//         {
//             GroupAgent a1 = new GroupAgent();
//             a1.show();
//             Agent b1 = new Agent();
//             b1.show();
//             GroupAgent a2 = new Agent();
//             a2.show();
//         }
//     }
// }


// using System;
// using System.Text.RegularExpressions;
// // using System.Globalization;

// namespace ConsoleApp
// {
//     class GroupAgent
//     {
//         public virtual void show()
//         {
//             Console.WriteLine("GroupAgent Created !!!");
//             Console.ReadLine();
//         }
        
//     }

//     class Agent : GroupAgent
//     {
//         public override void show()
//         {
//             Console.WriteLine("Agent Created !!!");
//             Console.ReadLine();
//         }   
//     }

//     class OLDExercise
//     {
//         static void Main(string[] args)
//         {
//             GroupAgent a1 = new GroupAgent();
//             a1.show();
//             Agent b1 = new Agent();
//             b1.show();
//             GroupAgent a2 = new Agent();
//             a2.show();
//         }
//     }
// }



// using System;
// using System.Text.RegularExpressions;
// // using System.Globalization;

// namespace ConsoleApp
// {
//     class GroupAgent
//     {
//         public void show()
//         {
//             Console.WriteLine("GroupAgent Created !!!");
//             Console.ReadLine();
//         }
        
//     }

//     class Agent : GroupAgent
//     {
//         public sealed override void show()
//         {
//             Console.WriteLine("Agent Created !!!");
//             Console.ReadLine();
//         }
//     }

//     class Booking : Agent 
//     {
//         public override void show()
//         {
//             Console.WriteLine("Booking Created !!!");
//             Console.ReadLine();
//         }
//     }

//     class OLDExercise
//     {
//         static void Main(string[] args)
//         {
//             GroupAgent a1 = new GroupAgent();
//             a1.show();
//             Agent b1 = new Agent();
//             b1.show();
//             GroupAgent a2 = new Booking();
//             a2.show();
//         }
//     }
// }








