// using System;

// namespace Generic
// {
//     class Gen<T>
//     {
//         T obj;
//         public Gen(T obj1)
//         {
//             this.obj = obj1;
//         }

//         public T Get()
//         {
//             return obj;
//         }

//         public void Show(T obj)
//         {
//             Console.WriteLine("Type of T: " + typeof(T));
//             Console.WriteLine("Value is :" + obj);
//         }
//     }

//     class TestGeneric
//     {
//         public static void Main(string[] args)
//         {
//             Gen<int>objdata;
//             objdata = new Gen<int>(500);
//             objdata.Show(600);

//             Gen<string>obj1;
//             obj1 = new Gen<string>("chars");
//             obj1.Show("hello");
//         }
//     }

// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;

namespace ProductDemo
{
    public class Product
    {
        public int Id { get; set; }
        public string ? Name { get; set; }
        public string ? Desc { get; set; }
        public int Price { get; set; }
        public bool isStock { get; set; }
    }

    public class ProductCatalog
    {
        private List<Product> products;

        public ProductCatalog()
        {
            // products = new List<Product>
            // {
            //     new Product { Id = 100, Name = "Laptop", Desc = "The best", isStock = true, Price = 10000 },
            //     new Product { Id = 101, Name = "Phone", Desc = "The best1", isStock = true, Price = 100900 },
            //     new Product { Id = 102, Name = "Desktop", Desc = "The best2", isStock = true, Price = 100800 },
            //     new Product { Id = 103, Name = "Ipad", Desc = "The best3", isStock = true, Price = 100400 },
            //     new Product { Id = 104, Name = "Car", Desc = "The best4", isStock = true, Price = 100200 },
            //     new Product { Id = 105, Name = "Iqube", Desc = "The best5", isStock = true, Price = 102000 }
            // };

            products = new List<Product>();
        }

        public void addProduct()
        {
            Product product = new Product();
            Console.WriteLine("Enter the ProductId : ");
            product.Id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the Name : ");
            product.Name = Console.ReadLine();

            Console.WriteLine("Enter the Desc : ");
            product.Desc = Console.ReadLine();

            Console.WriteLine("Enter the isStock : ");
            product.isStock = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine("Enter the Price : ");
            product.Price = Convert.ToInt32(Console.ReadLine());

            products.Add(product);
        }

        public bool DeleteData(int id)
        {
            var productid = products.FirstOrDefault(p => p.Id == id);
            if(productid != null)
            {
                return false;
                products.Remove(productid);
                return true;
            }
            return true;
        }

        public void Display()
        {
            foreach (var product in products)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("ID: " + product.Id);
                Console.WriteLine("Name: " + product.Name);
                Console.WriteLine("Description: " + product.Desc);
                Console.WriteLine("In Stock: " + product.isStock);
                Console.WriteLine("Price: " + product.Price);
                Console.WriteLine("--------------------------");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ProductCatalog obj = new ProductCatalog();
            int choice;
            do
            {
                Console.WriteLine("\n 1. Add Product");
                Console.WriteLine("\n 2. Display Product");
                Console.WriteLine("\n 3.Delete a product");
                Console.WriteLine("\n 4. Exit");
                Console.WriteLine("\n Enter Your Choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch(choice)
                {
                    case 1:
                        obj.addProduct();
                        break;
                    
                    case 2:
                        obj.Display();
                        break;

                    case 3:
                        Console.WriteLine("Enter id to delete");
                        int id = Convert.ToInt32(Console.ReadLine());
                        obj.DeleteData(id);
                        break;
                    
                    case 4:
                        Console.WriteLine("----- Exiting -----");
                        break;

                    default :
                        Console.WriteLine("////////////// INVALID CHOICE //////////////");
                        break;
                }
            }
            while(choice != 3);
        }
    }
}