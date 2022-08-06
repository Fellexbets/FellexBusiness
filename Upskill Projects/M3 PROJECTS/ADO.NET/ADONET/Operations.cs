using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using EFCore.Models;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace EFCore
{
    public class Operations
    {

        public static void RemoveIten()
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
                if (ProgramOperations.SelectedEntity == typeof(Customer))
                {
                    sqlConnection.Open();
                    Console.WriteLine("Select the ID of the Customer you want to remove");
                    string customerToRemove = Console.ReadLine();
                    string commandText = $"DELETE FROM CUSTOMERS WHERE CustomerID = {customerToRemove}";
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sqlCommand.ExecuteReader();
                    Console.WriteLine("Your iten was removed! :)");
                    sqlConnection.Close();
                }
                if (ProgramOperations.SelectedEntity == typeof(Employee))
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
                if (ProgramOperations.SelectedEntity == typeof(Shipper))
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
                if (ProgramOperations.SelectedEntity == typeof(Supplier))
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
        }

        public static void ListAll()
        {

            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {
                ProgramOperations.SelectEntities();
                if (ProgramOperations.SelectedEntity == typeof(Product))
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
                if (ProgramOperations.SelectedEntity == typeof(Customer))
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

                if (ProgramOperations.SelectedEntity == typeof(Employee))
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
                if (ProgramOperations.SelectedEntity == typeof(Shipper))
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
                if (ProgramOperations.SelectedEntity == typeof(Supplier))
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

        }


        public static void Update()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {
                ProgramOperations.SelectEntities();
                if (ProgramOperations.SelectedEntity == typeof(Product))
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
                if (ProgramOperations.SelectedEntity == typeof(Customer))
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

                    string commandText = $"UPDATE CUSTOMERS SET CompanyName ='{answer1}', ContactName ='{answer2}', ContactTitle = '{answer3}', Address ='{answer4}', City ='{answer5}', PostalCode='{answer6}', Country='{answer7}', Phone='{answer8}' WHERE CUSTOMERID = {customerToUpdate}";
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sqlCommand.ExecuteReader();
                    Console.WriteLine("Your iten was updated! :)");
                    sqlConnection.Close();

                    
                }
                if (ProgramOperations.SelectedEntity == typeof(Employee))
                {

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
                if (ProgramOperations.SelectedEntity == typeof(Supplier))
                {

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
                    Console.WriteLine("Your changes were saved! :)");
                }
                if (ProgramOperations.SelectedEntity == typeof(Shipper))
                {

                    Console.WriteLine("Write the ID of the shipper you want to update");
                    int shipperToUpdate = int.Parse(Console.ReadLine());
                    Console.WriteLine("What is the new shipper's company name?");
                    string answer1 = Console.ReadLine();
                    Console.WriteLine("What is the new Supplier's Phone?");
                    string answer2 = Console.ReadLine();

                    string commandText = $"UPDATE SHIPPERS SET COMPANYNAME ='{answer1}', PHONE = '{answer2}' WHERE SHIPPERID = {shipperToUpdate}";
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sqlCommand.ExecuteReader();
                    Console.WriteLine("Your iten was updated! :)");
                    sqlConnection.Close();
                    Console.WriteLine("Your changes were saved! :)");
                }
            }

        }


        public static void CreateIten()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {
                ProgramOperations.SelectEntities();
                if (ProgramOperations.SelectedEntity == typeof(Employee))
                {
                    sqlConnection.Open();
                    Console.WriteLine("Write the Employee's Last Name");
                    string answer1 = Console.ReadLine();
                    Console.WriteLine("write the Employee's First Name");
                    string answer2 = Console.ReadLine();
                    Console.WriteLine("write the Employee's Title");
                    string answer3 = Console.ReadLine();
                    Console.WriteLine("write the Employee's Adress");
                    string answer4 = Console.ReadLine();
                    Console.WriteLine("write the customers Country");
                    string answer5 = Console.ReadLine();
                    string commandText = $"INSERT INTO EMPLOYEES VALUES ('{answer1}', '{answer2}', '{answer3}', '{answer4}', '{answer5}')";
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sqlCommand.ExecuteReader();
                    Console.WriteLine("Your iten was created! :)");
                    sqlConnection.Close();

                }
                if (ProgramOperations.SelectedEntity == typeof(Customer))
                {
                    sqlConnection.Open();
                    Console.WriteLine("Write the customers name");
                    string answer1 = Console.ReadLine();
                    Console.WriteLine("write the customers title");
                    string answer2 = Console.ReadLine();
                    Console.WriteLine("write the customers address");
                    string answer3 = Console.ReadLine();
                    Console.WriteLine("write the customers city");
                    string answer4 = Console.ReadLine();
                    Console.WriteLine("write the customers country");
                    string answer5 = Console.ReadLine();
                    Console.WriteLine("write the customers phone");
                    string answer6 = Console.ReadLine();
                    Console.WriteLine("write the customers fax");
                    string answer7 = Console.ReadLine();
                    string commandText = $"INSERT INTO PRODUCTS VALUES ('{answer1}', '{answer2}', '{answer3}', '{answer4}', '{answer5}', '{answer6}', '{answer7}')";
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sqlCommand.ExecuteReader();
                    Console.WriteLine("Your iten was created! :)");
                    sqlConnection.Close();

                }
                if (ProgramOperations.SelectedEntity == typeof(Supplier))
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
                if (ProgramOperations.SelectedEntity == typeof(Product))
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
                if (ProgramOperations.SelectedEntity == typeof(Shipper))
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
        }

        public static void Close()
        {
            Program.EndProgram = true;
        }
        public static void SQLexercises()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {
                ProgramOperations.SelectExercicio();
                if ((ProgramOperations.SelectedExercise) == "1")
                {
                    sqlConnection.Open();
                    Console.WriteLine("Which shippers do we have? We have a table called Shippers. Return all the fields from all the shippers.");
                    string commandText = "SELECT * FROM SHIPPERS";
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sqlCommand.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"{query.GetString(1)}");
                    }
                    sqlConnection.Close();

                }
                if ((ProgramOperations.SelectedExercise) == "2")
                {
                    sqlConnection.Open();
                    Console.WriteLine("We�d like to see just the FirstName, LastName, and HireDate of all the employees with the Title of Sales Representative. Write a SQL statement that returns only those employees.");
                    string commandText = "SELECT FIRSTNAME, LASTNAME, HIREDATE FROM EMPLOYEES WHERE TITLE = 'SALES REPRESENTATIVE'";
                    SqlCommand command = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = command.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"{query.GetString(0)} {query.GetString(1)} {query.GetDateTime(2)}");
                    }
                    sqlConnection.Close();
                }
                if ((ProgramOperations.SelectedExercise) == "3")
                {
                    sqlConnection.Open();
                    Console.WriteLine("Now we�d like to see the same columns as above, but only for those employees that both have the title of Sales Representative, and also are in the United States.");
                    string commandText = "SELECT FIRSTNAME, LASTNAME, HIREDATE FROM EMPLOYEES WHERE TITLE = 'SALES REPRESENTATIVE' and COUNTRY = 'USA'";
                    SqlCommand cmd = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = cmd.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"{query.GetString(0)} {query.GetString(1)} {query.GetDateTime(2)}");
                    }
                    sqlConnection.Close();

                }
                if ((ProgramOperations.SelectedExercise) == "4")
                {
                    sqlConnection.Open();
                    Console.WriteLine("In the Suppliers table, show the SupplierID, ContactName, and ContactTitle for those Suppliers whose ContactTitle is not Marketing Manager.");
                    string commandText = "SELECT SUPPLIERID, CONTACTNAME, CONTACTTITLE FROM SUPPLIERS WHERE CONTACTTITLE != 'MARKETING MANAGER'";
                    SqlCommand sql = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sql.ExecuteReader();

                    while ((query.Read()))
                    {
                        Console.WriteLine($"{query.GetInt32(0)} {query.GetString(1)}, {query.GetString(2)}");
                    }
                    sqlConnection.Close();

                }
                if ((ProgramOperations.SelectedExercise) == "5")
                {
                    sqlConnection.Open();
                    Console.WriteLine(" In the products table, we�d like to see the ProductID and ProductName for those products where the ProductName includes the string �queso�.");
                    string commandText = "SELECT PRODUCTID, PRODUCTNAME FROM PRODUCTS WHERE PRODUCTNAME LIKE '%queso%'";
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sqlCommand.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"{query.GetInt32(0)}, {query.GetString(1)} ");
                    }
                    sqlConnection.Close();
                }
                if ((ProgramOperations.SelectedExercise) == "6")
                {
                    sqlConnection.Open();
                    Console.WriteLine(" For all the employees in the Employees table, show the FirstName, LastName, Title, and BirthDate. Order the results by BirthDate, so we have the oldest employees first.");
                    string commandText = "SELECT FIRSTNAME, LASTNAME, TITLE, BIRTHDATE FROM EMPLOYEES ORDER BY BIRTHDATE ASC";
                    SqlCommand cmd = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = cmd.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"{query.GetString(0)} {query.GetString(1)}, {query.GetString(2)}, {query.GetDateTime(3)},");
                    }
                    sqlConnection.Close();

                }
                if ((ProgramOperations.SelectedExercise) == "7")
                {
                    sqlConnection.Open();
                    Console.WriteLine("Show the FirstName and LastName columns from the Employees table, and then create a new column called FullName, showing FirstName and LastName joined together in one column, with a space in-between.");
                    string commandText = "SELECT CONCAT (FIRSTNAME,' ', LASTNAME) AS FullName FROM EMPLOYEES";
                    SqlCommand command = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = command.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine(query.GetString(0));
                    }
                    sqlConnection.Close();

                }
                if ((ProgramOperations.SelectedExercise) == "8")
                {
                    sqlConnection.Open();
                    Console.WriteLine("How many customers do we have in the Customers table? Show one value only, and don�t rely on getting the recordcount at the end of a resultset.");
                    string commandText = "SELECT COUNT (CUSTOMERID) FROM CUSTOMERS";
                    SqlCommand cmd = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = cmd.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"We have {query.GetInt32(0)} customers in the Customers table.");
                    }
                    sqlConnection.Close();
                }
                if ((ProgramOperations.SelectedExercise) == "9")
                {
                    sqlConnection.Open();
                    Console.WriteLine("Show a list of countries where the Northwind company has customers.");
                    string commandText = "SELECT DISTINCT COUNTRY FROM CUSTOMERS";
                    SqlCommand sql = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sql.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"{query.GetString(0)}");
                    }

                }
                if ((ProgramOperations.SelectedExercise) == "10")
                {
                    sqlConnection.Open();
                    Console.WriteLine("Show a list of all the different values in the Customers table for ContactTitles.");
                    string commandText = "SELECT DISTINCT CONTACTTITLE FROM CUSTOMERS";
                    SqlCommand sql = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sql.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"{query.GetString(0)}");
                    }


                    sqlConnection.Close();
                }
                if ((ProgramOperations.SelectedExercise) == "11")
                {
                    sqlConnection.Open();
                    Console.WriteLine("In the customers table, show the total number of customers per country and city.");
                    string commandText = "SELECT Country, COUNT (Country) AS customersfromcountry, City, COUNT (City) AS customersfromcity FROM Customers GROUP BY Country, City";
                    SqlCommand sql = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sql.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"{query.GetString(0)}, {query.GetInt32(1)}, {query.GetString(2)}, {query.GetInt32(3)}");
                    }

                }
                if ((ProgramOperations.SelectedExercise) == "12")
                {
                    sqlConnection.Open();
                    Console.WriteLine(" What products do we have in our inventory that should be reordered? For now, just use the fields UnitsInStock and ReorderLevel, where UnitsInStock is less than the ReorderLevel, ignoring the fields UnitsOnOrder and Discontinued. Order the results by ProductID.");
                    string commandText = "SELECT ProductName, ProductID, UnitsInStock FROM PRODUCTS WHERE UnitsinStock < ReorderLevel ORDER BY ProductID ASC";
                    SqlCommand sql = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sql.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"{query.GetString(0)}, {query.GetInt32(1)}, {query.GetInt16(2)}");
                    }

                }
                if ((ProgramOperations.SelectedExercise) == "13")
                {
                    sqlConnection.Open();
                    Console.WriteLine("Now we need to incorporate these fields—UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued—into our calculation. We’ll define “products that need reordering” with the following: UnitsInStock plus UnitsOnOrder are less than or equal to ReorderLevel, The Discontinued flag is false. ");
                    string commandText = "SELECT ProductName AS ProductsThatNeedReordering FROM PRODUCTS WHERE UnitsinStock + UnitsOnOrder <= ReorderLevel AND Discontinued = 0 ";
                    SqlCommand sql = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sql.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"{query.GetString(0)}");
                    }
                }


            }

        }

        public static void BestOfTheBest()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Program.connectionString))
            {
                ProgramOperations.SelectEntities();
                if (ProgramOperations.SelectedEntity == typeof(Product))
                {
                    sqlConnection.Open();
                    string commandText = "SELECT PRODUCTNAME FROM PRODUCTS WHERE UNITSONORDER > 70";
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = sqlCommand.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"{query.GetString(0)}");
                    }
                    sqlConnection.Close();
                }
                if (ProgramOperations.SelectedEntity == typeof(Employee))
                {
                    sqlConnection.Open();
                    string commandText = "SELECT TOP 1 BIRTHDATE, FIRSTNAME, LASTNAME FROM EMPLOYEES ORDER BY BIRTHDATE ASC";
                    SqlCommand cmd = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = cmd.ExecuteReader();

                    while (query.Read())

                    {
                        Console.WriteLine($"The best of the best employee this month was {query.GetString(1)} {query.GetString(2)}, because she is the oldest and this month she shared a lot of her experience with the new employees. She was born in {query.GetDateTime(0)} !!!");

                    }
                }
                if (ProgramOperations.SelectedEntity == typeof(Customer))
                {
                    sqlConnection.Open();
                    string commandText = "SELECT CONTACTNAME, CITY FROM CUSTOMERS WHERE CITY = 'BERLIN'";
                    SqlCommand command = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = command.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"The best of the best customer this month was {query.GetString(0)}, because she is from {query.GetString(1)}, and {query.GetString(1)} is NICE!");
                    }
                    sqlConnection.Close();

                }
                if (ProgramOperations.SelectedEntity == typeof(Shipper))
                {
                    sqlConnection.Open();
                    string commandText = "SELECT COMPANYNAME FROM SHIPPERS WHERE COMPANYNAME LIKE '%SPEED%'";
                    SqlCommand command = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = command.ExecuteReader();

                    while (query.Read())
                    {
                        Console.WriteLine($"The best of the best shipper this month was {query.GetString(0)}, because HE IS FAST!");

                    }
                    sqlConnection.Close();
                }
                if (ProgramOperations.SelectedEntity == typeof(Supplier))
                {
                    sqlConnection.Open();
                    string commandText = "SELECT COMPANYNAME, COUNTRY FROM SUPPLIERS WHERE COUNTRY = 'SPAIN'";
                    SqlCommand command = new SqlCommand(commandText, sqlConnection);
                    SqlDataReader query = command.ExecuteReader();


                    while (query.Read())
                    {
                        Console.WriteLine($"The best of the best suppliers this month was {query.GetString(0)}, because they are from {query.GetString(1)}, a country in which we have least representation !");

                    }
                    sqlConnection.Close();
                }
            }





        }




    }
}





