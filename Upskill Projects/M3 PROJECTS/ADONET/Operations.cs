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
            ProgramOperations.SelectEntities();
            Type toRemove = ProgramOperations.SelectedEntity;
            Entity obj = (Entity)Activator.CreateInstance(toRemove);
            obj.RemoveIten();
        }

        public static void ListAll()
        {

            ProgramOperations.SelectEntities();
            Type toList = ProgramOperations.SelectedEntity;
            Entity obj = (Entity)Activator.CreateInstance(toList);
            obj.ListAll();

        }


        public static void Update()
        {
            ProgramOperations.SelectEntities();
            Type toUpdate = ProgramOperations.SelectedEntity;
            Entity obj = (Entity)Activator.CreateInstance(toUpdate);
            obj.Update();
        }


        public static void CreateIten()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            ProgramOperations.SelectEntities();
            Type toCreate = ProgramOperations.SelectedEntity;
            Entity obj = (Entity)Activator.CreateInstance(toCreate);
            obj.Create();
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





