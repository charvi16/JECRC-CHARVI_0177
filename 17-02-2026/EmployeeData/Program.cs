// using System;

// namespace ConsoleApp
// {
//     class Employee
//     {
//         // Properties
//         public string Name { get; set; }
//         public int Id { get; set; }
//         public string Department { get; set; }
//         public double Salary { get; set; }
//         public string Position{ get; set; }
//         public DateTime DateofJoining{ get; set; }

//         // Method to take input
//         public void GetEmployeeData()
//         {
//             Console.WriteLine("Enter Employee Name:");
//             Name = Console.ReadLine();

//             Console.WriteLine("Enter Employee Id:");
//             Id = Convert.ToInt32(Console.ReadLine());

//             Console.WriteLine("Enter Employee Department:");
//             Department = Console.ReadLine();

//             Console.WriteLine("Enter Employee Salary:");
//             Salary = Convert.ToDouble(Console.ReadLine());

//             Console.WriteLine("Enter Employee Position:");
//             Position = Console.ReadLine();

//             Console.WriteLine("Enter Employee DateofJoining:");
//             Salary = Console.ReadLine();
//         }

//         // Method to display data
//         public void DisplayEmployeeData()
//         {
//             Console.WriteLine("Employee Name: " + Name);
//             Console.WriteLine("Employee Id: " + Id);
//             Console.WriteLine("Employee Department: " + Department);
//             Console.WriteLine("Employee Salary: " + Salary);
//             Console.WriteLine("Employee Position: " + Position);
//             Console.WriteLine("Employee DateofJoining: " + DateofJoining);

//         }
//     }

//     class Program
//     {
//         static void Main(string[] args)
//         {
//             Employee emp = new Employee();
//             emp.GetEmployeeData();
//             emp.DisplayEmployeeData();
//         }
//     }
// }

// using System;

// namespace ConsoleApp
// {
//     class Employee
//     {
//         // Properties
//         public string Name { get; set; }
//         public int Id { get; set; }
//         public string Department { get; set; }
//         public double Salary { get; set; }
//         public string Position { get; set; }
//         public DateTime DateofJoining { get; set; }

//         // Method to take input
//         public void GetEmployeeData()
//         {
//             Console.WriteLine("Enter Employee Name:");
//             Name = Console.ReadLine();

//             Console.WriteLine("Enter Employee Id:");
//             Id = Convert.ToInt32(Console.ReadLine());

//             Console.WriteLine("Enter Employee Department:");
//             Department = Console.ReadLine();

//             Console.WriteLine("Enter Employee Salary:");
//             Salary = Convert.ToDouble(Console.ReadLine());

//             Console.WriteLine("Enter Employee Position:");
//             Position = Console.ReadLine();

//             Console.WriteLine("Enter Employee Date of Joining (yyyy-MM-dd):");
//             DateofJoining = Convert.ToDateTime(Console.ReadLine());
//         }

//         // Method to display data
//         public void DisplayEmployeeData()
//         {
//             Console.WriteLine("Employee Details:");
//             Console.WriteLine("Employee Name: " + Name);
//             Console.WriteLine("Employee Id: " + Id);
//             Console.WriteLine("Employee Department: " + Department);
//             Console.WriteLine("Employee Salary: " + Salary);
//             Console.WriteLine("Employee Position: " + Position);
//             Console.WriteLine("Employee Date of Joining: " + DateofJoining.ToShortDateString());
//         }
//     }

//     class Program
//     {
//         static void Main(string[] args)
//         {
//             Employee emp = new Employee();
//             emp.GetEmployeeData();
//             emp.DisplayEmployeeData();
//         }
//     }
// }


// using System;

// public class HelloWorld
// {
//     public static void Main(string[] args)
//     {
//         string[] stringArray = new string[5] {"Csharp", "ASP.net", "EntityFrameWork", "ADO,net", "WCF"};
//         Array.Sort(stringArray);
//         foreach(string str in stringArray)
//         {
//             Console.Write(str + " ");
//         }
//     }
// }

// using System;

// public class HelloWorld
// {
//     public static void Main(string[] args)
//     {
//         int a = 10; int b =100;
//         if(a>b)
//         {
//             Console.WriteLine("a is greater");
//         }
//         else
//         {
//             Console.WriteLine("b is greater");
//         }
//     }
// }

// using System;

// public class HelloWorld
// {
//     public static void Main(string[] args)
//     {
//         string myInput;
//         int myInt;

//         begin:

//         Console.WriteLine("Please a entter a number between 1 and 3:");
//         myInput = Console.ReadLine();
//         myInt = Int32.Parse(myInput);

//         switch(myInt)
//         {
//             case 1:
//                 Console.WriteLine("Your number is {0}.", myInt);
//                 break;
//             case 2:
//                 Console.WriteLine("Your number is {0}.", myInt);
//                 break;
//             case 3:
//                 Console.WriteLine("Your number is {0}.", myInt);
//                 break;
//             default:
//                 Console.WriteLine("Your number {0} is not between 1 and 3.", myInt);
//                 break;
//         }
//     }
// }

//using System;

// public class HelloWorld
// {
//     public static void Main(string[] args)
//     {
//         int a =2;
//         do{
//             Console.WriteLine(a);
//         }
//         while(a<3);

//     }
// }


// using System;
// public class HelloWorld
// {
//     public static void Main(string[] args)
//     {
//         char letter1;
//         char letter2;
//         char letter3;

//         Console.Write("Enter letter: ");
//         letter1 = Convert.ToChar(Console.ReadLine());

//         Console.Write("Enter letter: ");
//         letter2 = Convert.ToChar(Console.ReadLine());

//         Console.Write("Enter letter: ");
//         letter3 = Convert.ToChar(Console.ReadLine());

//         Console.WriteLine("The letters in reverse order are: {0} {1} {2}", 
//                           letter3, letter2, letter1);
//     }
// }


// using System;

// class Program
// {
//     static void Main()
//     {
//         Console.Write("Enter a number: ");
//         int number = Convert.ToInt32(Console.ReadLine());

//         Console.Write("Enter the desired width: ");
//         int width = Convert.ToInt32(Console.ReadLine());

//         for (int i = width; i >= 1; i--)
//         {
//             for (int j = 1; j <= i; j++)
//             {
//                 Console.Write(number);
//             }
//             Console.WriteLine();
//         }
//     }
// }


// using System;

// class Program
// {
//     static void Main()
//     {
//         Console.Write("Input First number: ");
//         int num1 = Convert.ToInt32(Console.ReadLine());

//         Console.Write("Input Second number: ");
//         int num2 = Convert.ToInt32(Console.ReadLine());

//         if(num1 %2 ==0 && num2 %2 ==0)
//         {
//             Console.WriteLine("Even");
//         }
//         else
//         {
//             Console.WriteLine("Odd");
//         }
//     }
// }

// using System;

// namespace ConsoleApp
// {
//     class Product
//     {
//         // Properties
//         public int Id { get; set; }
//         public string Name { get; set; }
//         public DateTime ExpiryDate { get; set; }
//         public int Quantity { get; set; }
//         public double Price { get; set; }
//         public bool BrandISO { get; set; }

//         // Method to take input
//         public void GetProductData()
//         {
//             Console.WriteLine("Enter Product Id:");
//             Id = Convert.ToInt32(Console.ReadLine());

//             Console.WriteLine("Enter Product Name:");
//             Name = Console.ReadLine();

//             Console.WriteLine("Enter Expiry Date (yyyy-mm-dd):");
//             ExpiryDate = Convert.ToDateTime(Console.ReadLine());

//             Console.WriteLine("Enter Product Quantity:");
//             Quantity = Convert.ToInt32(Console.ReadLine());

//             Console.WriteLine("Enter Product Price:");
//             Price = Convert.ToDouble(Console.ReadLine());

//             Console.WriteLine("Is Brand ISO Certified? (true/false):");
//             BrandISO = Convert.ToBoolean(Console.ReadLine());
//         }

//         // Method to display data
//         public void DisplayProductData()
//         {
//             Console.WriteLine("\n--- Product Details ---");
//             Console.WriteLine("Product Id: " + Id);
//             Console.WriteLine("Product Name: " + Name);
//             Console.WriteLine("Expiry Date: " + ExpiryDate.ToShortDateString());
//             Console.WriteLine("Quantity: " + Quantity);
//             Console.WriteLine("Price: " + Price);
//             Console.WriteLine("Brand ISO Certified: " + BrandISO);
//         }
//     }

//     class Program
//     {
//         static void Main(string[] args)
//         {
//             Product prod = new Product();
//             prod.GetProductData();
//             prod.DisplayProductData();
//         }
//     }
// }


// using System;
// class OLDExercise
// {
//     int a;
//     int b;

//     // Method to take input
//     public void GetNumbers()
//     {
//         Console.Write("Input First number: ");
//         a = Convert.ToInt32(Console.ReadLine());

//         Console.Write("Input Second number: ");
//         b = Convert.ToInt32(Console.ReadLine());
//     }
    
//     public void Addition()
//     {
//         int sum = a + b;
//         Console.WriteLine("Sum =" + sum);
//     }
//     public void Subtraction()
//     {
//         int sum = a - b;
//         Console.WriteLine("Subtraction =" + sum);
//     }
//     public void Multiply()
//     {
//         int sum = a * b;
//         Console.WriteLine("Multiplication =" + sum);
//     }
//     public void Division()
//     {
//         int sum = a % b;
//         Console.WriteLine("Division =" + sum);
//     }
// }

// class Demo
// {
//     static void Main()
//     {
//         OLDExercise obj = new OLDExercise();
//         obj.GetNumbers();
//         obj.Addition();
//         obj.Subtraction();
//         obj.Multiply();
//         obj.Division();
//     }
// }

// using System;

// class OLDExercise
// {
//     int a;
//     int b;

//     // Method to take input
//     public void GetNumbers()
//     {
//         Console.Write("Input First number: ");
//         a = Convert.ToInt32(Console.ReadLine());

//         Console.Write("Input Second number: ");
//         b = Convert.ToInt32(Console.ReadLine());
//     }

//     public void Addition()
//     {
//         int sum = a + b;
//         Console.WriteLine("Sum = " + sum);
//     }

//     public void Subtraction()
//     {
//         int result = a - b;
//         Console.WriteLine("Subtraction = " + result);
//     }

//     public void Multiply()
//     {
//         int result = a * b;
//         Console.WriteLine("Multiplication = " + result);
//     }

//     public void Division()
//     {
//         if (b != 0)
//         {
//             int result = a / b;
//             Console.WriteLine("Division = " + result);
//         }
//         else
//         {
//             Console.WriteLine("Cannot divide by zero");
//         }
//     }
// }

// class Demo
// {
//     static void Main()
//     {
//         OLDExercise obj = new OLDExercise();
//         obj.GetNumbers();
//         obj.Addition();
//         obj.Subtraction();
//         obj.Multiply();
//         obj.Division();
//     }
// }

// using System;
// class OLDExercise
// {
//     private static int number;
//     public static int Number{
//         get
//         {
//             return number;
//         }
//     }
//     static OLDExercise()
//     {
//         Random r = new Random();
//         number = r.Next();
//     }
// }

// class Program
// {
//     static void Main(string[] args)
//     {
//         Console.WriteLine("Static Number = " + OLDExercise.Number);
//     }
// }


// using System;
// class OLDExercise
// {
//     private int number;
//     public int Number{
//         get
//         {
//             return number;
//         }
//     }
//     public OLDExercise()
//     {
//         Random r = new Random();
//         number = r.Next();
//     }
// }

// class Program
// {
//     static void Main(string[] args)
//     {
//         OLDExercise a = new OLDExercise();
//         Console.WriteLine("Static Number = " + a.Number);
//     }
// }


// using System;
// class OLDExercise
// {
//     private int number;
//     public int Number{
//         get
//         {
//             return number;
//         }
//     }
//     public OLDExercise()
//     {
//         Random r = new Random();
//         number = r.Next();
//     }
//     public OLDExercise(int seed)
//     {
//         Random r = new Random(seed);
//         number = r.Next();
//     }
// }

// class Program
// {
//     static void Main(string[] args)
//     {
//         OLDExercise a = new OLDExercise();
//         Console.WriteLine("Static Number = " + a.Number);
//         OLDExercise b = new OLDExercise(500);
//         Console.WriteLine("Static Number = " + b.Number);
//     }
// }


using System;

namespace ConsoleApp
{
    class Product
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool BrandISO { get; set; }

        // Parameterized Constructor
        public Product(int id, string name, DateTime expiryDate,
                       int quantity, double price, bool brandISO)
        {
            Id = id;
            Name = name;
            ExpiryDate = expiryDate;
            Quantity = quantity;
            Price = price;
            BrandISO = brandISO;
        }

        // Method to display data
        public void DisplayProductData()
        {
            Console.WriteLine("\n--- Product Details ---");
            Console.WriteLine("Product Id: " + Id);
            Console.WriteLine("Product Name: " + Name);
            Console.WriteLine("Expiry Date: " + ExpiryDate.ToShortDateString());
            Console.WriteLine("Quantity: " + Quantity);
            Console.WriteLine("Price: " + Price);
            Console.WriteLine("Brand ISO Certified: " + BrandISO);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Product Id:");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Product Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Expiry Date (yyyy-mm-dd):");
            DateTime expiryDate = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Enter Product Quantity:");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Product Price:");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Is Brand ISO Certified? (true/false):");
            bool brandISO = Convert.ToBoolean(Console.ReadLine());

            // Creating object using parameterized constructor
            Product prod = new Product(id, name, expiryDate, quantity, price, brandISO);

            prod.DisplayProductData();
        }
    }
}
