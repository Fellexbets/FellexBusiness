using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore.Models
{
    public partial class Product : Entity
    {
        public Product()
        {
            
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        
        public virtual Supplier Supplier { get; set; }


        public override string ToString() => ($"{ProductId}: {ProductName}, its priced at {UnitPrice} $. We have {UnitsInStock} of those in stock. ");

        public override void Create()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {

                sqlConnection.Open();
                Console.WriteLine("What is the product name?");
                string answer1 = Console.ReadLine();
                Console.WriteLine("What is the supplier ID?");
                string answer2 = Console.ReadLine();
                Console.WriteLine("What is the quantity per unit?");
                string answer3 = Console.ReadLine();
                Console.WriteLine("What is the  Unit Price?");
                short answer4 = short.Parse(Console.ReadLine());
                Console.WriteLine("What is the  quantity of units in stock?");
                short answer5 = short.Parse(Console.ReadLine());
                Console.WriteLine("What is the quantity of units on order?");
                short answer6 = short.Parse(Console.ReadLine());
                Console.WriteLine("What is the reorder level?");
                short answer7 = short.Parse(Console.ReadLine());
                Console.WriteLine("Is it discontinued? true or false?");
                bool answer8 = bool.Parse(Console.ReadLine());
                string commandText = $"INSERT INTO PRODUCTS VALUES ('{answer1}', '{answer2}', '{answer3}', '{answer4}', '{answer5}', '{answer6}', '{answer7}', '{answer8}')";
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                SqlDataReader query = sqlCommand.ExecuteReader();
                Console.WriteLine("Your iten was created! :)");
                sqlConnection.Close();
            }
        }
        public override void ListAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {

                sqlConnection.Open();
                string commandText = "SELECT * FROM EMPLOYEES";
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                SqlDataReader query = sqlCommand.ExecuteReader();
                while (query.Read())
                {
                    Console.WriteLine($"{ query["FirstName"] } { query["LastName"]}");
                }
                sqlConnection.Close();
            }
        }
        public override void RemoveIten()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {

                ProgramOperations.SelectEntities();
                if (ProgramOperations.SelectedEntity == typeof(Product))
                {
                    sqlConnection.Open();
                    Console.WriteLine("Select the ID of the Product you want to remove");
                    int productToRemove = int.Parse(Console.ReadLine());
                    string commandText = $"DELETE FROM PRODUCTS WHERE ProductID = {productToRemove}";
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sqlCommand.ExecuteReader();
                    Console.WriteLine("Your iten was removed! :)");
                    sqlConnection.Close();
                }
            }
        }
        public override void Update()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {

                sqlConnection.Open();
                Console.WriteLine("Write the ID of the product you want to update");
                int productToUpdate = int.Parse(Console.ReadLine());

                Console.WriteLine("What is the new product name?");
                string answer1 = Console.ReadLine();
                Console.WriteLine("What is the new supplier ID?");
                int answer2 = int.Parse(Console.ReadLine());
                Console.WriteLine("What is the new quantity per unit?");
                string answer3 = Console.ReadLine();
                Console.WriteLine("What is the new Unit Price?");
                short answer4 = short.Parse(Console.ReadLine());
                Console.WriteLine("What is the new quantity of units in stock?");
                short answer5 = short.Parse(Console.ReadLine());
                Console.WriteLine("What is the new quantity of units on order?");
                short answer6 = short.Parse(Console.ReadLine());
                Console.WriteLine("What is the new reorder level?");
                short answer7 = short.Parse(Console.ReadLine());

                string commandText = $"UPDATE PRODUCTS SET ProductName ='{answer1}', SupplierID ={answer2}, QuantityPerUnit = {answer3}, UnitPrice ={answer4}, UnitsInStock ={answer5}, UnitsOnOrder={answer6}, ReorderLevel={answer7} WHERE ProductID = {productToUpdate}";
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                SqlDataReader query = sqlCommand.ExecuteReader();
                Console.WriteLine("Your iten was updated! :)");
                sqlConnection.Close();
            }
        }
    }

}
