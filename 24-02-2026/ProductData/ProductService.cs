// using MySql.Data.MySqlClient;
// using System;

// namespace ProductManagementSystem
// {
//     internal class ProductService
//     {
//         private readonly string connectionString =
//         "server=127.0.0.1;port=3307;database=EmployeeDB;user=root;password=MySQLRoot@309";

//         ProductModel product = new ProductModel();

//         public void Run()
//         {
//             while (true)
//             {
//                 Console.Clear();

//                 Console.WriteLine("=== Product Management System ===");

//                 Console.WriteLine("1. View All Products");
//                 Console.WriteLine("2. Insert Product");
//                 Console.WriteLine("3. Update Product");
//                 Console.WriteLine("4. Delete Product");
//                 Console.WriteLine("5. Search By ID");
//                 Console.WriteLine("6. Search By Category");
//                 Console.WriteLine("7. Sort Products");
//                 Console.WriteLine("8. Exit");

//                 Console.Write("Enter choice: ");

//                 int choice = Convert.ToInt32(Console.ReadLine());

//                 switch (choice)
//                 {
//                     case 1:
//                         ViewAllProducts();
//                         break;

//                     case 2:
//                         InsertProduct();
//                         break;

//                     case 3:
//                         UpdateProduct();
//                         break;

//                     case 4:
//                         DeleteProduct();
//                         break;

//                     case 5:
//                         SearchById();
//                         break;

//                     case 6:
//                         SearchByCategory();
//                         break;

//                     case 7:
//                         SortProducts();
//                         break;

//                     case 8:
//                         return;

//                     default:
//                         Console.WriteLine("Invalid Choice");
//                         break;
//                 }

//                 Console.WriteLine("\nPress Enter...");
//                 Console.ReadLine();
//             }
//         }

//         // View All
//         public void ViewAllProducts()
//         {
//             using SqlConnection conn = new SqlConnection(connectionString);
//             conn.Open();

//             string query = "SELECT * FROM Products";

//             using SqlCommand cmd = new SqlCommand(query, conn);
//             using SqlDataReader reader = cmd.ExecuteReader();

//             Console.WriteLine("\n=== Product List ===");

//             while (reader.Read())
//             {
//                 Console.WriteLine(
//                     $"ID: {reader.GetInt32(0)} | " +
//                     $"Name: {reader.GetString(1)} | " +
//                     $"Category: {reader.GetString(2)} | " +
//                     $"Price: {reader.GetDecimal(3)} | " +
//                     $"InStock: {reader.GetBoolean(4)}");
//             }
//         }


//         // Insert
//         public void InsertProduct()
//         {
//             Console.Write("Enter Name: ");
//             product.Name = Console.ReadLine();

//             Console.Write("Enter Category: ");
//             product.Category = Console.ReadLine();

//             Console.Write("Enter Price: ");
//             product.Price = Convert.ToDecimal(Console.ReadLine());

//             Console.Write("In Stock (true/false): ");
//             product.InStock = Convert.ToBoolean(Console.ReadLine());

//             using SqlConnection conn = new SqlConnection(connectionString);
//             conn.Open();

//             string query =
//             "INSERT INTO Products(Name,Category,Price,InStock) VALUES(@Name,@Category,@Price,@InStock)";

//             using SqlCommand cmd = new SqlCommand(query, conn);

//             cmd.Parameters.AddWithValue("@Name", product.Name);
//             cmd.Parameters.AddWithValue("@Category", product.Category);
//             cmd.Parameters.AddWithValue("@Price", product.Price);
//             cmd.Parameters.AddWithValue("@InStock", product.InStock);

//             cmd.ExecuteNonQuery();

//             Console.WriteLine("Product Inserted Successfully");
//         }


//         // Update
//         public void UpdateProduct()
//         {
//             Console.Write("Enter Product ID: ");
//             product.Id = Convert.ToInt32(Console.ReadLine());

//             Console.Write("Enter Name: ");
//             product.Name = Console.ReadLine();

//             Console.Write("Enter Category: ");
//             product.Category = Console.ReadLine();

//             Console.Write("Enter Price: ");
//             product.Price = Convert.ToDecimal(Console.ReadLine());

//             Console.Write("In Stock (true/false): ");
//             product.InStock = Convert.ToBoolean(Console.ReadLine());


//             using SqlConnection conn = new SqlConnection(connectionString);
//             conn.Open();

//             string query =
//             "UPDATE Products SET Name=@Name,Category=@Category,Price=@Price,InStock=@InStock WHERE Id=@Id";

//             using SqlCommand cmd = new SqlCommand(query, conn);

//             cmd.Parameters.AddWithValue("@Id", product.Id);
//             cmd.Parameters.AddWithValue("@Name", product.Name);
//             cmd.Parameters.AddWithValue("@Category", product.Category);
//             cmd.Parameters.AddWithValue("@Price", product.Price);
//             cmd.Parameters.AddWithValue("@InStock", product.InStock);

//             int rows = cmd.ExecuteNonQuery();

//             Console.WriteLine(rows > 0 ?
//                 "Product Updated"
//                 :
//                 "Product Not Found");
//         }


//         // Delete
//         public void DeleteProduct()
//         {
//             Console.Write("Enter Product ID: ");

//             int id = Convert.ToInt32(Console.ReadLine());

//             using SqlConnection conn = new SqlConnection(connectionString);
//             conn.Open();

//             string query = "DELETE FROM Products WHERE Id=@Id";

//             using SqlCommand cmd = new SqlCommand(query, conn);

//             cmd.Parameters.AddWithValue("@Id", id);

//             int rows = cmd.ExecuteNonQuery();

//             Console.WriteLine(rows > 0 ?
//                 "Product Deleted"
//                 :
//                 "Product Not Found");
//         }


//         // Search by ID
//         public void SearchById()
//         {
//             Console.Write("Enter Product ID: ");

//             int id = Convert.ToInt32(Console.ReadLine());

//             using SqlConnection conn = new SqlConnection(connectionString);
//             conn.Open();

//             string query = "SELECT * FROM Products WHERE Id=@Id";

//             using SqlCommand cmd = new SqlCommand(query, conn);

//             cmd.Parameters.AddWithValue("@Id", id);

//             using SqlDataReader reader = cmd.ExecuteReader();

//             if (reader.Read())
//             {
//                 Console.WriteLine(
//                     $"ID: {reader.GetInt32(0)} | " +
//                     $"Name: {reader.GetString(1)} | " +
//                     $"Category: {reader.GetString(2)} | " +
//                     $"Price: {reader.GetDecimal(3)} | " +
//                     $"InStock: {reader.GetBoolean(4)}");
//             }
//             else
//             {
//                 Console.WriteLine("Product Not Found");
//             }
//         }


//         // Search by Category
//         public void SearchByCategory()
//         {
//             Console.Write("Enter Category: ");

//             string category = Console.ReadLine();

//             using SqlConnection conn = new SqlConnection(connectionString);
//             conn.Open();

//             string query =
//             "SELECT * FROM Products WHERE Category=@Category";

//             using SqlCommand cmd = new SqlCommand(query, conn);

//             cmd.Parameters.AddWithValue("@Category", category);

//             using SqlDataReader reader = cmd.ExecuteReader();

//             while (reader.Read())
//             {
//                 Console.WriteLine(
//                     $"ID: {reader.GetInt32(0)} | " +
//                     $"Name: {reader.GetString(1)} | " +
//                     $"Category: {reader.GetString(2)} | " +
//                     $"Price: {reader.GetDecimal(3)} | " +
//                     $"InStock: {reader.GetBoolean(4)}");
//             }
//         }


//         // Sort Products
//         public void SortProducts()
//         {
//             Console.WriteLine("1. Sort By Price");
//             Console.WriteLine("2. Sort By Name");

//             int choice = Convert.ToInt32(Console.ReadLine());

//             string query = "";

//             if (choice == 1)
//                 query = "SELECT * FROM Products ORDER BY Price";

//             else if (choice == 2)
//                 query = "SELECT * FROM Products ORDER BY Name";

//             using SqlConnection conn = new SqlConnection(connectionString);
//             conn.Open();

//             using SqlCommand cmd = new SqlCommand(query, conn);

//             using SqlDataReader reader = cmd.ExecuteReader();

//             while (reader.Read())
//             {
//                 Console.WriteLine(
//                     $"ID: {reader.GetInt32(0)} | " +
//                     $"Name: {reader.GetString(1)} | " +
//                     $"Category: {reader.GetString(2)} | " +
//                     $"Price: {reader.GetDecimal(3)} | " +
//                     $"InStock: {reader.GetBoolean(4)}");
//             }
//         }

//     }
// }





using MySql.Data.MySqlClient;
using System;

namespace ProductManagementSystem
{
    internal class ProductService
    {
        private readonly string connectionString =
        "server=127.0.0.1;port=3307;database=ProductDB;user=root;password=MySQLRoot@309";

        ProductModel product = new ProductModel();

        public void Run()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== Product Management System ===");

                Console.WriteLine("1. View All Products");
                Console.WriteLine("2. Insert Product");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("5. Search By ID");
                Console.WriteLine("6. Search By Category");
                Console.WriteLine("7. Sort Products");
                Console.WriteLine("8. Exit");

                Console.Write("Enter choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1: ViewAllProducts(); break;
                    case 2: InsertProduct(); break;
                    case 3: UpdateProduct(); break;
                    case 4: DeleteProduct(); break;
                    case 5: SearchById(); break;
                    case 6: SearchByCategory(); break;
                    case 7: SortProducts(); break;
                    case 8: return;
                    default: Console.WriteLine("Invalid Choice"); break;
                }

                Console.WriteLine("\nPress Enter...");
                Console.ReadLine();
            }
        }


        // View All
        public void ViewAllProducts()
        {
            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            string query = "SELECT * FROM Products";

            using MySqlCommand cmd = new MySqlCommand(query, conn);
            using MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== Product List ===");

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader.GetInt32("Id")} | " +
                    $"Name: {reader.GetString("Name")} | " +
                    $"Category: {reader.GetString("Category")} | " +
                    $"Price: {reader.GetDecimal("Price")} | " +
                    $"InStock: {reader.GetBoolean("InStock")}");
            }
        }


        // Insert
        public void InsertProduct()
        {
            Console.Write("Enter Name: ");
            product.Name = Console.ReadLine();

            Console.Write("Enter Category: ");
            product.Category = Console.ReadLine();

            Console.Write("Enter Price: ");
            product.Price = Convert.ToDecimal(Console.ReadLine());

            Console.Write("In Stock (true/false): ");
            product.InStock = Convert.ToBoolean(Console.ReadLine());

            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            string query =
            "INSERT INTO Products(Name,Category,Price,InStock) VALUES(@Name,@Category,@Price,@InStock)";

            using MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Category", product.Category);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@InStock", product.InStock);

            cmd.ExecuteNonQuery();

            Console.WriteLine("Product Inserted Successfully");
        }


        // Update
        public void UpdateProduct()
        {
            Console.Write("Enter Product ID: ");
            product.Id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Name: ");
            product.Name = Console.ReadLine();

            Console.Write("Enter Category: ");
            product.Category = Console.ReadLine();

            Console.Write("Enter Price: ");
            product.Price = Convert.ToDecimal(Console.ReadLine());

            Console.Write("In Stock (true/false): ");
            product.InStock = Convert.ToBoolean(Console.ReadLine());

            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            string query =
            "UPDATE Products SET Name=@Name,Category=@Category,Price=@Price,InStock=@InStock WHERE Id=@Id";

            using MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Id", product.Id);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Category", product.Category);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@InStock", product.InStock);

            int rows = cmd.ExecuteNonQuery();

            Console.WriteLine(rows > 0 ?
                "Product Updated"
                :
                "Product Not Found");
        }


        // Delete
        public void DeleteProduct()
        {
            Console.Write("Enter Product ID: ");

            int id = Convert.ToInt32(Console.ReadLine());

            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            string query = "DELETE FROM Products WHERE Id=@Id";

            using MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Id", id);

            int rows = cmd.ExecuteNonQuery();

            Console.WriteLine(rows > 0 ?
                "Product Deleted"
                :
                "Product Not Found");
        }


        // Search by ID
        public void SearchById()
        {
            Console.Write("Enter Product ID: ");

            int id = Convert.ToInt32(Console.ReadLine());

            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            string query = "SELECT * FROM Products WHERE Id=@Id";

            using MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Id", id);

            using MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader.GetInt32("Id")} | " +
                    $"Name: {reader.GetString("Name")} | " +
                    $"Category: {reader.GetString("Category")} | " +
                    $"Price: {reader.GetDecimal("Price")} | " +
                    $"InStock: {reader.GetBoolean("InStock")}");
            }
            else
            {
                Console.WriteLine("Product Not Found");
            }
        }


        // Search by Category
        public void SearchByCategory()
        {
            Console.Write("Enter Category: ");

            string category = Console.ReadLine();

            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            string query =
            "SELECT * FROM Products WHERE Category=@Category";

            using MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Category", category);

            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader.GetInt32("Id")} | " +
                    $"Name: {reader.GetString("Name")} | " +
                    $"Category: {reader.GetString("Category")} | " +
                    $"Price: {reader.GetDecimal("Price")} | " +
                    $"InStock: {reader.GetBoolean("InStock")}");
            }
        }


        // Sort
        public void SortProducts()
        {
            Console.WriteLine("1. Sort By Price");
            Console.WriteLine("2. Sort By Name");

            int choice = Convert.ToInt32(Console.ReadLine());

            string query = choice == 1 ?
                "SELECT * FROM Products ORDER BY Price" :
                "SELECT * FROM Products ORDER BY Name";

            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand(query, conn);

            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader.GetInt32("Id")} | " +
                    $"Name: {reader.GetString("Name")} | " +
                    $"Category: {reader.GetString("Category")} | " +
                    $"Price: {reader.GetDecimal("Price")} | " +
                    $"InStock: {reader.GetBoolean("InStock")}");
            }
        }

    }
}