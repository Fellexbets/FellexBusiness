using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore.Models
{
    public partial class Supplier : Entity
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string HomePage { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public override void Create()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {
                sqlConnection.Open();
                Console.WriteLine("Write the supplier's name");
                string answer1 = Console.ReadLine();
                Console.WriteLine("write the supplier's area of work");
                string answer2 = Console.ReadLine();
                Console.WriteLine("write the supplier's Contact Title");
                string answer3 = Console.ReadLine();
                Console.WriteLine("write the supplier's Adress");
                string answer4 = Console.ReadLine();
                Console.WriteLine("write the supplier's City");
                string answer5 = Console.ReadLine();
                Console.WriteLine("write the supplier's Country");
                string answer6 = Console.ReadLine();
                Console.WriteLine("write the supplier's Phone");
                string answer7 = Console.ReadLine();
                string commandText = $"INSERT INTO SUPPLIERS VALUES ('{answer1}', '{answer2}', '{answer3}', '{answer4}', '{answer5}', '{answer6}', '{answer7}')";
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
                string commandText = "SELECT * FROM SUPPLIERS";
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
                Console.WriteLine("Select the ID of the Supplier you want to remove");
                int supplierToRemove = int.Parse(Console.ReadLine());
                string commandText = $"DELETE FROM EMPLOYEES WHERE ShipperID = {supplierToRemove}";
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
                Console.WriteLine("Write the ID of the supplier you want to update");
                int supplierToUpdate = int.Parse(Console.ReadLine());
                Console.WriteLine("What is the new Supplier's company name?");
                string answer1 = Console.ReadLine();
                Console.WriteLine("What is the new Supplier's Contact Name?");
                string answer2 = Console.ReadLine();
                Console.WriteLine("What is the new Supplier's Contact Title?");
                string answer3 = Console.ReadLine();
                Console.WriteLine("What is the new Supplier's Adress?");
                string answer4 = Console.ReadLine();
                Console.WriteLine("What is the new Supplier's City?");
                string answer5 = Console.ReadLine();
                Console.WriteLine("What is the new Supplier's Postal Code?");
                string answer6 = Console.ReadLine();
                Console.WriteLine("What is the new Supplier's Country?");
                string answer7 = Console.ReadLine();
                Console.WriteLine("What is the new Supplier's Phone?");
                string answer8 = Console.ReadLine();
                string commandText = $"UPDATE SUPPLIERS SET CompanyName ='{answer1}', ContactName ='{answer2}', ContactTitle = '{answer3}', Address ='{answer4}', City ='{answer5}', PostalCode='{answer6}', Country='{answer7}', Phone='{answer8}' WHERE CUSTOMERID = {supplierToUpdate}";

                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                SqlDataReader query = sqlCommand.ExecuteReader();
                Console.WriteLine("Your iten was updated! :)");
                sqlConnection.Close();
            }
        }
    }
}
