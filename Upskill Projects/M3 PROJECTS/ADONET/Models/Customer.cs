using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore.Models
{
    public partial class Customer : Entity
    {
        public Customer()
        {
            
        }

        public string CustomerId { get; set; }
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


        public override void Create()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {

                sqlConnection.Open();
                Console.WriteLine("Write the customers ID");
                string answer1 = Console.ReadLine();
                Console.WriteLine("Write the customers Company Name");
                string answer2 = Console.ReadLine();
                Console.WriteLine("Write the customers Contact name");
                string answer3 = Console.ReadLine();
                Console.WriteLine("write the customers Contact title");
                string answer4 = Console.ReadLine();
                Console.WriteLine("write the customers address");
                string answer5 = Console.ReadLine();
                Console.WriteLine("write the customers city");
                string answer6 = Console.ReadLine();
                Console.WriteLine("write the customers region");
                string answer7 = Console.ReadLine();
                Console.WriteLine("write the customers postal code");
                string answer8 = Console.ReadLine();
                Console.WriteLine("write the customers country");
                string answer9 = Console.ReadLine();
                Console.WriteLine("write the customers phone");
                string answer10 = Console.ReadLine();
                Console.WriteLine("write the customers fax");
                string answer11 = Console.ReadLine();
                string commandText = $"INSERT INTO CUSTOMERS VALUES (@answer1, @answer2, @answer3, @answer4, @answer5, @answer6, @answer7, @answer8,@answer9, @answer10, @answer11)"; 
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@answer1", answer1);

                sqlCommand.Parameters.AddWithValue("@answer2", answer2); 

                sqlCommand.Parameters.AddWithValue("@answer3", answer3);

                sqlCommand.Parameters.AddWithValue("@answer4", answer4);

                sqlCommand.Parameters.AddWithValue("@answer5", answer5);

                sqlCommand.Parameters.AddWithValue("@answer6", answer6);

                sqlCommand.Parameters.AddWithValue("@answer7", answer7);

                sqlCommand.Parameters.AddWithValue("@answer8", answer8);

                sqlCommand.Parameters.AddWithValue("@answer9", answer9);

                sqlCommand.Parameters.AddWithValue("@answer10", answer10);

                sqlCommand.Parameters.AddWithValue("@answer11", answer11);

                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Your iten was created! :)");
                sqlConnection.Close();
            }
        }
        public override void ListAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {
                sqlConnection.Open();
                string commandText = "SELECT * FROM PRODUCTS";
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                SqlDataReader query = sqlCommand.ExecuteReader();
                while (query.Read())
                {
                    Console.WriteLine($"{ query["ProductName"] }");
                }
                sqlConnection.Close();
            }
        }
        public override void RemoveIten()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {

                sqlConnection.Open();
                Console.WriteLine("Select the ID of the Customer you want to remove");
                string customerToRemove = Console.ReadLine();
                string commandText = $"DELETE FROM CUSTOMERS WHERE CustomerID = '{customerToRemove}'";
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Your iten was removed! :)");
                sqlConnection.Close();
            }
        }
        public override void Update()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {

                sqlConnection.Open();
                Console.WriteLine("Write the ID of the customer you want to update");
                string customerToUpdate = Console.ReadLine();
                Console.WriteLine("What is the new customer company name?");
                string answer1 = Console.ReadLine();
                Console.WriteLine("What is the new Customer Contact Name?");
                string answer2 = Console.ReadLine();
                Console.WriteLine("What is the new customer Contact Title?");
                string answer3 = Console.ReadLine();
                Console.WriteLine("What is the new customer Adress?");
                string answer4 = Console.ReadLine();
                Console.WriteLine("What is the new customer City?");
                string answer5 = Console.ReadLine();
                Console.WriteLine("What is the new customer Postal Code?");
                string answer6 = Console.ReadLine();
                Console.WriteLine("What is the new customer Country?");
                string answer7 = Console.ReadLine();
                Console.WriteLine("What is the new customer Phone?");
                string answer8 = Console.ReadLine();

                string commandText = $"UPDATE CUSTOMERS SET CompanyName =@answer1, ContactName =@answer2, ContactTitle = @answer3, Address =@answer4, City =@answer5, PostalCode=@answer6, Country=@answer7, Phone=@answer8 WHERE CUSTOMERID = '{customerToUpdate}'";
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@answer1", answer1);
                sqlCommand.Parameters.AddWithValue("@answer2", answer2);
                sqlCommand.Parameters.AddWithValue("@answer3", answer3);
                sqlCommand.Parameters.AddWithValue("@answer4", answer4);
                sqlCommand.Parameters.AddWithValue("@answer5", answer5);
                sqlCommand.Parameters.AddWithValue("@answer6", answer6);
                sqlCommand.Parameters.AddWithValue("@answer7", answer7);
                sqlCommand.Parameters.AddWithValue("@answer8", answer8);
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Your iten was updated! :)");
                sqlConnection.Close();
            }
        }
    }
}
