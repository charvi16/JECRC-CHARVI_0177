// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

// using System;
// namespace InterfaceDemo
// {
//     interface IArea
//     {
//         void CalcArea(double radius);
//     }
//     interface IVolume
//     {
//         void CalcVolume(int side);
//     }
//     public class CircleCube : IArea, IVolume
//     {
//         public void CalcArea(double radius)
//         {
//             double pie = 3.14;
//             double result;
//             result = pie * radius * radius;
//             Console.WriteLine(result);
//         }
//         public void CalcVolume(int side)
//         {
//             int result;
//             result = side * side * side;
//             Console.WriteLine(result);
//         }
//     }
//     class TestApp
//     {
//         public static void Main(string[] args)
//         {
//             CircleCube obj = new CircleCube();
//             double radius;
//             int side;
//             Console.WriteLine("Enter the value of the radius:");
//             radius = Convert.ToDouble(Console.ReadLine());
//             obj.CalcArea(radius);
//             Console.WriteLine("Enter the value of the side:");
//             side = Convert.ToInt32(Console.ReadLine());
//             obj.CalcVolume(side);
//             Console.ReadLine();
//             Console.WriteLine ("Try programiz.pro");
//         }
//     }
// }


// using System;
// namespace AbstractDemo
// {
//     public abstract class CalcArea
//     {
//         public abstract void Area(double radius);
//         public void bFunction()
//         {
//             Console.WriteLine("It is not a non abstract method");
//         }
//     }
//     interface IVolume
//     {
//         void CalcVolume(int side);
//     }
//     public class CircleCube : CalcArea, IVolume
//     {
//         public override void Area(double radius)
//         {
//             double pie = 3.14;
//             double result;
//             result = pie * radius * radius;
//             Console.WriteLine(result);
//         }
//         public void CalcVolume(int side)
//         {
//             int result;
//             result = side * side * side;
//             Console.WriteLine(result);
//         }
//     }
//     class TestApp
//     {
//         public static void Main(string[] args)
//         {
//             CircleCube obj = new CircleCube();
//             double radius;
//             int side;
//             Console.WriteLine("Enter the value of the radius:");
//             radius = Convert.ToDouble(Console.ReadLine());
//             obj.Area(radius);
//             obj.bFunction();
//             Console.WriteLine("Enter the value of the side:");
//             side = Convert.ToInt32(Console.ReadLine());
//             obj.CalcVolume(side);
//             Console.ReadLine();
//             Console.WriteLine ("Try programiz.pro");
//         }
//     }
// }


// using System;

// namespace AbstractDemo
// {
//     // Abstract base class
//     abstract class DrawingObject
//     {
//         // Abstract method
//         public abstract void Draw();
//     }

//     // Derived class Line
//     class Line : DrawingObject
//     {
//         public override void Draw()
//         {
//             Console.WriteLine("I'm a Line.");
//         }
//     }

//     // Derived class Circle
//     class Circle : DrawingObject
//     {
//         public override void Draw()
//         {
//             Console.WriteLine("I'm a Circle.");
//         }
//     }

//     // Derived class Square
//     class Square : DrawingObject
//     {
//         public override void Draw()
//         {
//             Console.WriteLine("I'm a Square.");
//         }
//     }

//     class Program
//     {
//         static void Main(string[] args)
//         {
//             // Create array of DrawingObject
//             DrawingObject[] objects = new DrawingObject[]
//             {
//                 new Line(),
//                 new Circle(),
//                 new Square()
//             };

//             // Loop and call Draw() method
//             foreach (DrawingObject obj in objects)
//             {
//                 obj.Draw();
//             }

//             Console.ReadLine();
//         }
//     }
// }



// using System;

// namespace VirtualDemo
// {
//     // Base class (not abstract now)
//     class DrawingObject
//     {
//         // Virtual method
//         public virtual void Draw()
//         {
//             Console.WriteLine("Drawing an object.");
//         }
//     }

//     // Derived class Line
//     class Line : DrawingObject
//     {
//         public override void Draw()
//         {
//             Console.WriteLine("I'm a Line.");
//         }
//     }

//     // Derived class Circle
//     class Circle : DrawingObject
//     {
//         public override void Draw()
//         {
//             Console.WriteLine("I'm a Circle.");
//         }
//     }

//     // Derived class Square
//     class Square : DrawingObject
//     {
//         public override void Draw()
//         {
//             Console.WriteLine("I'm a Square.");
//         }
//     }

//     class Program
//     {
//         static void Main(string[] args)
//         {
//             // Create array of base class type
//             DrawingObject[] objects = new DrawingObject[]
//             {
//                 new Line(),
//                 new Circle(),
//                 new Square()
//             };

//             // Loop and call Draw()
//             foreach (DrawingObject obj in objects)
//             {
//                 obj.Draw();
//             }

//             Console.ReadLine();
//         }
//     }
// }



// using System;

// namespace VirtualDemo
// {
//     // Base class (not abstract now)
//     class DrawingObject
//     {
//         // Virtual method
//         public virtual void Draw()
//         {
//             Console.WriteLine("Drawing an object.");
//         }
//     }

//     // Derived class Line
//     class Line : DrawingObject
//     {
//         public override void Draw()
//         {
//             Console.WriteLine("I'm a Line.");
//         }
//     }

//     // Derived class Circle
//     class Circle : DrawingObject
//     {
//         public override void Draw()
//         {
//             Console.WriteLine("I'm a Circle.");
//         }
//     }

//     // Derived class Square
//     class Square : DrawingObject
//     {
//         public override void Draw()
//         {
//             Console.WriteLine("I'm a Square.");
//         }
//     }

//     class Program
//     {
//         static void Main(string[] args)
//         {
//             // Create array of base class type
//             DrawingObject[] objects = new DrawingObject[]
//             {
//                 new Line(),
//                 new Circle(),
//                 new Square()
//             };

//             // Loop and call Draw()
//             foreach (DrawingObject obj in objects)
//             {
//                 obj.Draw();
//             }

//             Console.ReadLine();
//         }
//     }
// }



// namespace DelegateDemo
// {
//     public delegate void ArithmaticOperation(int x, int y);
//     class UsingDelegate
//     {
//         // public delegate void ArithmaticOperation(int x, int y);
//         static void AddSimple()
//         {
//             Console.WriteLine("");
//         }
//         static void Addition(int x, int y)
//         {
//             Console.WriteLine(x + y);
//         }
//         static void Subtraction(int x, int y)
//         {
//             Console.WriteLine(x - y);
//         }
//         static void Multiplication(int x, int y)
//         {
//             Console.WriteLine(x * y);
//         }
//         static void Division(int x, int y)
//         {
//             Console.WriteLine(x / y);
//         }
//         static void Main(string[] args)
//         {
//             // ArithmaticOperation obj1 = new ArithmaticOperation(Addition);
//             // obj1(45, 90);
//             // ArithmaticOperation obj2 = new ArithmaticOperation(Subtraction);
//             // obj2(45, 90);
//             ArithmaticOperation obj = new ArithmaticOperation(Addition);
//             obj += new ArithmaticOperation(Subtraction);
//             obj += new ArithmaticOperation(Multiplication);
//             obj += new ArithmaticOperation(Division);
//             obj(45,90);
//             Console.ReadLine();

//         }
//     }
// }


// using System;

// namespace DelegateDemo
// {
//     // 1. Create delegate
//     public delegate void NotificationSender(string message);

//     // 2. Create Notifiers class
//     class Notifiers
//     {
//         public static void SendEmail(string message)
//         {
//             Console.WriteLine("Email Sent: " + message);
//         }

//         public static void SendSMS(string message)
//         {
//             Console.WriteLine("SMS Sent: " + message);
//         }

//         public static void SendPushNotification(string message)
//         {
//             Console.WriteLine("Push Notification Sent: " + message);
//         }
//     }

//     // 3. Create NotificationManager class
//     class NotificationManager
//     {
//         public void NotifyUser(string message, NotificationSender sender)
//         {
//             // Invoke delegate
//             sender(message);
//         }
//     }

//     class Program
//     {
//         static void Main(string[] args)
//         {
//             // 4. Create object of NotificationManager
//             NotificationManager manager = new NotificationManager();

//             // Send notifications
//             manager.NotifyUser("Welcome via Email!", Notifiers.SendEmail);
//             manager.NotifyUser("Welcome via SMS!", Notifiers.SendSMS);
//             manager.NotifyUser("Welcome via Push Notification!", Notifiers.SendPushNotification);

//             Console.ReadLine();
//         }
//     }
// }


// using System.Collections;
// namespace ArrayListDemo
// {
//     class UsingArrayList
//     {
//         static void Main(string[] args)
//         {
//             ArrayList listdata1 = new ArrayList();
//             listdata1.Add(100);
//             listdata1.Add(101);
//             listdata1.Add(102);
//             listdata1.Add(103);
//             listdata1.Add(104);
//             listdata1.Add("Dotnet");
//             foreach (object i in listdata1)
//             {
//                 Console.WriteLine(i);
//             }

//             ArrayList listdata2 = new ArrayList();
//             listdata2.Add(200);
//             listdata2.Add(201);
//             listdata2.Add(202);
//             listdata2.Add(203);
//             listdata2.Add(204);
//             listdata2.Add("Java");
//             foreach (object i in listdata2)
//             {
//                 Console.WriteLine(i);
//             }
//             listdata1.AddRange(listdata2);
//             foreach (object i in listdata1)
//             {
//                 Console.WriteLine(i);
//             }
//         }
//     }
// }


// using System.Collections;
// namespace ArrayListDemo
// {
//     class UsingArrayList
//     {
//         static void Main(string[] args)
//         {
//             ArrayList listdata1 = new ArrayList();
//             listdata1.Add(100);
//             listdata1.Add(101);
//             listdata1.Add(102);
//             listdata1.Add(103);
//             listdata1.Add(104);
//             listdata1.Add("Dotnet");
//             foreach (object i in listdata1)
//             {
//                 Console.WriteLine(i);
//             }

//             ArrayList listdata2 = new ArrayList();
//             listdata2.Add(200);
//             listdata2.Add(201);
//             listdata2.Add(202);
//             listdata2.Add(203);
//             listdata2.Add(204);
//             listdata2.Add("Java");
//             foreach (object i in listdata2)
//             {
//                 Console.WriteLine(i);
//             }
//             listdata1.AddRange(listdata2);
//             foreach (object i in listdata1)
//             {
//                 Console.WriteLine(i);
//             }
//             string order = " Order#1001 | Laptop | Dell | 75000";

//             Console.WriteLine(order);

//             string trimOrder = order.Trim();
//             Console.WriteLine(trimOrder);

//             Console.WriteLine(order.Length);
//             Console.WriteLine(trimOrder.Length);

//             string[] parts = trimOrder.Split('|');
//             Console.WriteLine("After Split");
//             foreach (var item in parts)
//             {
//                 Console.WriteLine(item.Trim());
//             }
//         }
//     }
// }


// using System;

// namespace OrderProcessingDemo
// {
//     // Abstract base class
//     abstract class OrderProcessor
//     {
//         // Properties
//         public int OrderId { get; set; }
//         public double Amount { get; set; }

//         // Abstract methods
//         public abstract void ProcessPayment();
//         public abstract void GenerateInvoice();
//         public abstract void SendNotification();

//         // Concrete method
//         public void DisplayOrderDetails()
//         {
//             Console.WriteLine("Order ID: " + OrderId);
//             Console.WriteLine("Amount: " + Amount);
//         }
//     }

//     // Derived class
//     class OnlineOrder : OrderProcessor
//     {
//         public override void ProcessPayment()
//         {
//             Console.WriteLine("Processing online payment of Rs. " + Amount);
//         }

//         public override void GenerateInvoice()
//         {
//             Console.WriteLine("Generating digital invoice for Order ID: " + OrderId);
//         }

//         public override void SendNotification()
//         {
//             Console.WriteLine("Sending email notification for Order ID: " + OrderId);
//         }
//     }

//     class Program
//     {
//         static void Main(string[] args)
//         {
//             // Parent reference, child object
//             OrderProcessor order = new OnlineOrder();

//             // Assign values
//             order.OrderId = 101;
//             order.Amount = 5000;

//             // Call methods
//             order.DisplayOrderDetails();
//             order.ProcessPayment();
//             order.GenerateInvoice();
//             order.SendNotification();

//             Console.ReadLine();
//         }
//     }
// }


// using System;

// namespace OrderProcessingDemo
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             int [] a = new int[4];
//             try
//             {
//                 Console.WriteLine("Before Exeception is generated");
//                 for (int i =0; i < 10; i++)
//                 {
//                     a[i] = i;
//                     Console.WriteLine("a[{0}]: {1}", i, a[i]);
//                 }
//                 Console.WriteLine("this will not be displayed");
//             }
//             catch (IndexOutOfRangeException)
//             {
//                 Console.WriteLine("Index out of bounds");
//             }
//             Console.WriteLine("After catch statement");
//         }
//     }
// }


// using System;

// namespace OrderProcessingDemo
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             int [] a = {4,8,16,32,64,128,256,512};
//             int [] b = {2,0,4,4,0,8};
//             for (int i=0; i<a.Length; i++)
//             {
//                 try
//                 {
//                     Console.WriteLine(a[i] + " / " + b[i] + " is " + a[i]/b[i]);
//                 }
//                 catch(DivideByZeroException)
//                 {
//                     Console.WriteLine("Can not divide by zero");
//                 }
//                 catch(IndexOutOfRangeException)
//                 {
//                     Console.WriteLine("Not matching element found");
//                 }
//             }
//         }
//     }
// }

// using System;

// namespace OrderProcessingDemo
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             int [] a = {4,8,16,32,64,128,256,512};
//             int [] b = {2,0,4,4,0,8};
//             for (int i=0; i<a.Length; i++)
//             {
//                 try
//                 {
//                     Console.WriteLine(a[i] + " / " + b[i] + " is " + a[i]/b[i]);
//                 }
//                 catch(DivideByZeroException)
//                 {
//                     Console.WriteLine("Some exception occur");
//                 }
//             }
//         }
//     }
// }


// using System;


// namespace OrderProcessingDemo
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             int [] a = {4,8,16,32,64,128,256,512};
//             int [] b = {2,0,4,4,0,8};
//             for (int i=0; i<a.Length; i++)
//             {
//                 try
//                 {
//                     Console.WriteLine(a[i] + " / " + b[i] + " is " + a[i]/b[i]);
//                 }
//                 catch(DivideByZeroException)
//                 {
//                     Console.WriteLine("Can not divide by zero");
//                 }
//             }
//         }
//     }
// }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CustomExceptionExampleCode
{
    class MyException: Exception 
    {
        public MyException(string Message): base (Message) {}
        public MyException() {}
        public MyException(string Message, Exception inner): base (Message,inner) {}
    }
    class Program
    {
        static void Main(string[] args) 
        {
            int a = 50;
            int b= 10;
            int k = a/ b;
            try 
            {
                if (k < 10) 
                { 
                    throw new MyException ("value of k is less than 10");
                }
            } 
            catch (MyException e) 
            {
                Console.WriteLine("Caught My Exception"); 
                Console.WriteLine (e.Message);
                Console.Read();
            }
        }
    }
}

