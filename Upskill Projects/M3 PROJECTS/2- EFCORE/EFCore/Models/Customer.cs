using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
            using (NorthwindContext db = new NorthwindContext())
            {
                Customer customer1 = new Customer();
                Console.WriteLine("Write the customers ID");
                customer1.CustomerId = Console.ReadLine();
                Console.WriteLine("Write the customers Company name");
                customer1.CompanyName = Console.ReadLine();
                Console.WriteLine("Write the customers Contact name");
                customer1.ContactName = Console.ReadLine();
                Console.WriteLine("write the customers title");
                customer1.ContactTitle = Console.ReadLine();
                Console.WriteLine("write the customers address");
                customer1.Address = Console.ReadLine();
                Console.WriteLine("write the customers city");
                customer1.City = Console.ReadLine();
                Console.WriteLine("write the customers country");
                customer1.Country = Console.ReadLine();
                Console.WriteLine("write the customers postal code");
                customer1.PostalCode = Console.ReadLine();
                Console.WriteLine("write the customers phone");
                customer1.Phone = Console.ReadLine();
                db.Add(customer1);
                db.SaveChanges();
                Console.WriteLine("Your iten was created! :)");
            }
        }

        public override void Update()
        {
            Console.ForegroundColor = ConsoleColor.White;
            using (NorthwindContext db = new NorthwindContext())
            {

                Console.WriteLine("Write the ID of the customer you want to update");
                string customerToUpdate = Console.ReadLine();
                Console.WriteLine("What is the new customer company name?");
                db.Customers.Find(customerToUpdate).CompanyName = Console.ReadLine();
                Console.WriteLine("What is the new customer contact name?");
                db.Customers.Find(customerToUpdate).ContactName = Console.ReadLine();
                Console.WriteLine("What is the new customer contact title?");
                db.Customers.Find(customerToUpdate).ContactTitle = Console.ReadLine();
                Console.WriteLine("What is the new customer's address?");
                db.Customers.Find(customerToUpdate).Address = Console.ReadLine();
                Console.WriteLine("What is the new customer's city?");
                db.Customers.Find(customerToUpdate).City = Console.ReadLine();
                Console.WriteLine("What is the new customer's postal code?");
                db.Customers.Find(customerToUpdate).PostalCode = Console.ReadLine();
                Console.WriteLine("What is the new customer's country?");
                db.Customers.Find(customerToUpdate).Country = Console.ReadLine();
                Console.WriteLine("What is the new customers phone?");
                db.Customers.Find(customerToUpdate).Phone = Console.ReadLine();
                db.SaveChanges();
                Console.WriteLine("Your changes were saved! :)");
            }
        }

        public override void RemoveIten()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            using (NorthwindContext db = new NorthwindContext())
            {

                Console.WriteLine("Select the ID of the Customer you want to remove");
                int customerToRemove = int.Parse(Console.ReadLine());
                db.Customers.Remove(db.Customers.Find(customerToRemove));
                db.SaveChanges();
                Console.WriteLine("Your iten was removed! :)");

            }
        }

        public override void ListAll()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            using (NorthwindContext db = new NorthwindContext())
            {
                var customers = db.Customers
                                           .AsNoTracking()
                                          .Select(customer => new { customer.ContactName, customer.Country, customer.Phone })
                                          .ToList();

                foreach (var customer in customers)
                {
                    Console.WriteLine($"Our dear and most loved customer {customer.ContactName} is from {customer.Country}. Their phone contact is {customer.Phone}.");
                }
            }
        }

    }
}
