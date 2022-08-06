using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using EFCore.Models;
using System.Linq.Expressions;




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
            Entity obj  = (Entity)Activator.CreateInstance(toUpdate);
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

        public static void BestOfTheBest()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            using (NorthwindContext db = new NorthwindContext())
            {
                ProgramOperations.SelectEntities();
                if (ProgramOperations.SelectedEntity == typeof(Product))
                {
                    IQueryable<Product> products = db.Products
                                                 .AsNoTracking()
                                                 .OrderByDescending(product => product.UnitsOnOrder)
                                                 .Take(1);
                    foreach (Product product in products)
                    {
                        Console.WriteLine($"The best of the best product this month was {product.ProductName}, because it was ordered over {product.UnitsOnOrder} times!!!");

                    }
                }
                if (ProgramOperations.SelectedEntity == typeof(Employee))
                {
                    IQueryable<Employee> employees = db.Employees
                                                 .AsNoTracking()
                                                 .OrderBy(product => product.BirthDate)
                                                 .Where(product => product.BirthDate != null)
                                                 .Take(1);
                    foreach (Employee employee in employees)
                    {
                        Console.WriteLine($"The best of the best employee this month was {employee.FirstName} {employee.LastName}, because she is the oldest and this month she shared a lot of her experience with the new employees. She was born in {employee.BirthDate}!!!");

                    }
                }
                if (ProgramOperations.SelectedEntity == typeof(Customer))
                {
                    IQueryable<Customer> customers = db.Customers
                                                 .AsNoTracking()
                                                 .Where(customer => customer.City == "Berlin");

                    foreach (Customer customer in customers)
                    {
                        Console.WriteLine($"The best of the best customer this month was {customer.ContactName}, because she is from {customer.City}, and {customer.City} is NICE!");

                    }
                }
                if (ProgramOperations.SelectedEntity == typeof(Shipper))
                {
                    IQueryable<Shipper> shippers = db.Shippers
                                                 .AsNoTracking()
                                                 .Where(shipper => shipper.CompanyName.Contains("Speed"));

                    foreach (Shipper shipper in shippers)
                    {
                        Console.WriteLine($"The best of the best shipper this month was {shipper.CompanyName}, because HE IS FAST!");

                    }
                }
                if (ProgramOperations.SelectedEntity == typeof(Supplier))
                {
                    var suppliers = db.Suppliers
                                                 .AsNoTracking().AsEnumerable()
                                                 .Select(supplier => new { supplier.Country, supplier.CompanyName })
                                                 .OrderBy(supplier => supplier.Country.Count())
                                                 .OrderBy(supplier => supplier.Country)
                                                 .Take(1);
                  

                    foreach (var supplier in suppliers)
                    {
                        Console.WriteLine($"The best of the best suppliers this month was {supplier.CompanyName}, because they are from {supplier.Country}, a country in which we have least representation !");

                    }
                }
            }


        }

        public static void SQLexercises()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            using (NorthwindContext db = new NorthwindContext())
            {
                ProgramOperations.SelectExercicio();
                if (int.Parse(ProgramOperations.SelectedExercise) == 1)
                {
                    Console.WriteLine("Which shippers do we have? We have a table called Shippers. Return all the fields from all the shippers.");
                    IEnumerable<Shipper> shippers = db.Shippers
                                                 .AsNoTracking();

                    foreach (Shipper shipper in shippers)
                    {
                        Console.WriteLine($"{shipper.ShipperId}  { shipper.CompanyName}  { shipper.Phone}");
                    }

                }
                if (int.Parse(ProgramOperations.SelectedExercise) == 2)
                {
                    Console.WriteLine("We�d like to see just the FirstName, LastName, and HireDate of all the employees with the Title of Sales Representative. Write a SQL statement that returns only those employees.");
                    var ex2 = db.Employees
                                .AsNoTracking()
                                .Where(ex2 => ex2.Title == "Sales Representative");

                    foreach (var x in ex2)
                    {
                        Console.WriteLine($"{x.FirstName} {x.LastName} {x.HireDate}");
                    }

                }
                if (int.Parse(ProgramOperations.SelectedExercise) == 3)
                {
                    Console.WriteLine("Now we�d like to see the same columns as above, but only for those employees that both have the title of Sales Representative, and also are in the United States.");
                    var ex2 = db.Employees
                                .AsNoTracking()
                                .Where(ex2 => ex2.Title == "Sales Representative" && ex2.Country == "USA");

                    foreach (var x in ex2)
                    {
                        Console.WriteLine($"{x.FirstName} {x.LastName} {x.HireDate}");
                    }

                }
                if (int.Parse(ProgramOperations.SelectedExercise) == 4)
                {
                    Console.WriteLine("In the Suppliers table, show the SupplierID, ContactName, and ContactTitle for those Suppliers whose ContactTitle is not Marketing Manager.");
                    var ex4 = db.Suppliers
                             .AsNoTracking()
                             .Where(supplier => supplier.ContactTitle != "Marketing Manager")
                             .Select(supplier => new { supplier.SupplierId, supplier.ContactName, supplier.ContactTitle });

                    foreach (var x in ex4)
                    {
                        Console.WriteLine(x);
                    }


                }
                if (int.Parse(ProgramOperations.SelectedExercise) == 5)
                {
                    Console.WriteLine(" In the products table, we�d like to see the ProductID and ProductName for those products where the ProductName includes the string �queso�.");

                    var products = db.Products
                                   .AsNoTracking()
                                   .Where(product => product.ProductName.Contains("queso"))
                                   .Select(product => new { product.ProductId, product.ProductName });
                    foreach (var x in products)
                    {
                        Console.WriteLine(x);
                    }

                }
                if (int.Parse(ProgramOperations.SelectedExercise) == 6)
                {
                    Console.WriteLine(" For all the employees in the Employees table, show the FirstName, LastName, Title, and BirthDate. Order the results by BirthDate, so we have the oldest employees first.");

                    var employees = db.Employees
                                   .AsNoTracking()
                                   .OrderBy(employee => employee.BirthDate)
                                   .Select(employee => new { employee.FirstName, employee.LastName, employee.Title, employee.BirthDate });
                    foreach (var x in employees)
                    {
                        Console.WriteLine(x);
                    }

                }
                if (int.Parse(ProgramOperations.SelectedExercise) == 7)
                {
                    Console.WriteLine("Show the FirstName and LastName columns from the Employees table, and then create a new column called FullName, showing FirstName and LastName joined together in one column, with a space in-between.");
                    var employees = db.Employees
                                    .AsNoTracking()
                                    .Select(employee => new { employee.FirstName, employee.LastName });
                    foreach (var x in employees)
                    {
                        Console.WriteLine($"{x.FirstName.Union(x.LastName)}");
                    }



                }
                if (int.Parse(ProgramOperations.SelectedExercise) == 8)
                {
                    Console.WriteLine("How many customers do we have in the Customers table? Show one value only, and don�t rely on getting the recordcount at the end of a resultset.");
                    var employees = db.Employees
                                    .AsNoTracking()
                                    .Select(customer => customer.EmployeeId);
                    
                    
                        Console.WriteLine(employees.Count());

                }
                if (int.Parse(ProgramOperations.SelectedExercise) == 9)
                {
                    Console.WriteLine("Show a list of countries where the Northwind company has customers.");
                    var customers = db.Customers
                                    .AsNoTracking()
                                    .AsEnumerable()
                                    .Select(customer => customer.Country)
                                    .Distinct();

                    foreach (var customer in customers)
                    {
                        Console.WriteLine(customer);
                    }

                }
                if (int.Parse(ProgramOperations.SelectedExercise) == 10)
                {
                    Console.WriteLine("Show a list of all the different values in the Customers table for ContactTitles. Also include a count for each ContactTitle. This is similar in concept to the previous question �Countries where there are customers�, except we now want a count for each ContactTitle.");
                    var customers = db.Customers
                                    .AsNoTracking()
                                    .Select(customer => customer.ContactTitle)
                                    .Distinct();

                    foreach (var customer in customers)
                    {
                        Console.WriteLine(customer);
                    }
                    Console.WriteLine(customers.Count());

                }
                if (int.Parse(ProgramOperations.SelectedExercise) == 11)
                {
                    Console.WriteLine("In the customers table, show the total number of customers per country and city.");
                    var customers = db.Customers
                                   .AsNoTracking()
                                   .AsEnumerable()
                                   .Select(customer => new { customer.Country, V = customer.Country.Count(), customer.City, C = customer.City.Count() })
                                   .Distinct();

                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"On the country { customer.Country} we have  {customer.V} customers. In the city of {customer.City} we have  {customer.C} customers");
                        
                    }


                }
                if (int.Parse(ProgramOperations.SelectedExercise) == 12)
                {
                    Console.WriteLine(" What products do we have in our inventory that should be reordered? For now, just use the fields UnitsInStock and ReorderLevel, where UnitsInStock is less than the ReorderLevel, ignoring the fields UnitsOnOrder and Discontinued. Order the results by ProductID.");
                    var products = db.Products
                                    .AsNoTracking()
                                    .Where(product => product.UnitsInStock < product.ReorderLevel)
                                    .OrderBy(product => product.ProductId);
                                   
  
                    foreach (var product in products)
                    {
                        Console.WriteLine($"The Product {product.ProductName}, ID:{ product.ProductId} needs needs to be reordered.");
                    }
                   
                }
                if (int.Parse(ProgramOperations.SelectedExercise) == 13)
                {
                    Console.WriteLine("Now we need to incorporate these fields—UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued—into our calculation. We’ll define “products that need reordering” with the following: UnitsInStock plus UnitsOnOrder are less than or equal to ReorderLevel, The Discontinued flag is false. ");
                    var products = db.Products
                                    .AsNoTracking()
                                    .Where(product => product.UnitsInStock + product.UnitsOnOrder <= product.ReorderLevel && product.Discontinued == false)
                                    .OrderBy(product => product.ProductId);


                    foreach (var product in products)
                    {
                        Console.WriteLine($"The Product {product.ProductName}, ID: { product.ProductId} ACTUALLY needs needs to be reordered.");
                    }

                }


            }

        }

    }
}

