using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore.Models
{
    public partial class Shipper : Entity
    {
        public Shipper()
        {
            
        }

        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public override void Create()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {

                sqlConnection.Open();
                Console.WriteLine("What is the Company Name?");
                string answer1 = Console.ReadLine();
                Console.WriteLine("What is the Phone number?");
                string answer2 = Console.ReadLine();
                string commandText = $"INSERT INTO SHIPPERS VALUES ('{answer1}', '{answer2}')";
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
                string commandText = "SELECT * FROM SHIPPERS";
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                SqlDataReader query = sqlCommand.ExecuteReader();
                while (query.Read())
                {
                    Console.WriteLine($"{ query["CompanyName"] }");
                }
                sqlConnection.Close();
            }
        }
        public override void RemoveIten()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {

                sqlConnection.Open();
                Console.WriteLine("Select the ID of the Shipper you want to remove");
                int shipperToRemove = int.Parse(Console.ReadLine());
                string commandText = $"DELETE FROM EMPLOYEES WHERE ShipperID = {shipperToRemove}";
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                SqlDataReader query = sqlCommand.ExecuteReader();
                Console.WriteLine("Your iten was removed! :)");
                sqlConnection.Close();
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
