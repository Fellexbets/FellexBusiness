using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore.Models
{
    public partial class Employee : Entity
    {
        public Employee()
        {
           
        }

        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public int? ReportsTo { get; set; }
        public string PhotoPath { get; set; }

        public virtual Employee ReportsToNavigation { get; set; }
        public virtual ICollection<Employee> InverseReportsToNavigation { get; set; }

        public override void Create()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {

                sqlConnection.Open();
                Console.WriteLine("Write the Employee's Last Name");
                string answer1 = Console.ReadLine();
                Console.WriteLine("write the Employee's First Name");
                string answer2 = Console.ReadLine();
                Console.WriteLine("write the Employee's Title");
                string answer3 = Console.ReadLine();
                Console.WriteLine("write the Employee's Title of Courtesy");
                string answer4 = Console.ReadLine();
                Console.WriteLine("write the customers Birthdate");
                DateTime answer5 = DateTime.Parse(Console.ReadLine());
                string commandText = $"INSERT INTO EMPLOYEES VALUES (@answer1, @answer2, @answer3, @answer4, @answer5)";
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@answer1", answer1);

                sqlCommand.Parameters.AddWithValue("@answer2", answer2);

                sqlCommand.Parameters.AddWithValue("@answer3", answer3);

                sqlCommand.Parameters.AddWithValue("@answer4", answer4);

                sqlCommand.Parameters.AddWithValue("@answer5", answer5);

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
                string commandText = "SELECT * FROM CUSTOMERS";
                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                SqlDataReader query = sqlCommand.ExecuteReader();
                while (query.Read())
                {
                    Console.WriteLine($"{ query["ContactName"] }");
                }
                sqlConnection.Close();
            }
        }
        public override void RemoveIten()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {

                sqlConnection.Open();
                Console.WriteLine("Select the ID of the Employee you want to remove");
                int employeeToRemove = int.Parse(Console.ReadLine());
                string commandText = $"DELETE FROM EMPLOYEES WHERE EmployeeID = {employeeToRemove}";
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
                Console.WriteLine("Write the ID of the employee you want to update");
                int employeeToUpdate = int.Parse(Console.ReadLine());
                Console.WriteLine("What is the new employee's Last Name?");
                string answer1 = Console.ReadLine();
                Console.WriteLine("What is the new employee's First Name?");
                string answer2 = Console.ReadLine();
                Console.WriteLine("What is the new employee's Title?");
                string answer3 = Console.ReadLine();
                Console.WriteLine("What is the new employee's Title of Courtesy?");
                string answer4 = Console.ReadLine();
                Console.WriteLine("What is the new employee's Birthdate?");
                DateTime answer5 = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("What is the new employee's Hire date?");
                DateTime answer6 = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("What is the new employee's Adress?");
                string answer7 = Console.ReadLine();
                Console.WriteLine("What is the new employee's City?");
                string answer8 = Console.ReadLine();
                Console.WriteLine("What is the new employee's Posta Code?");
                string answer9 = Console.ReadLine();
                Console.WriteLine("What is the new employee's Country?");
                string answer10 = Console.ReadLine();
                string commandText = $"UPDATE CUSTOMERS SET LastName ='{answer1}', FirstName ='{answer2}', Title = '{answer3}', TitleOfCourtesy ='{answer4}', BirthDate ={answer5}, HireDate={answer6}, Adress='{answer7}', City='{answer8}', PostalCode='{answer9}', Country='{answer10}' WHERE EmployeeID = {employeeToUpdate}";

                SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                SqlDataReader query = sqlCommand.ExecuteReader();
                Console.WriteLine("Your iten was updated! :)");
                sqlConnection.Close();
                Console.WriteLine("Your changes were saved! :)");
            }
        }
    }
}
