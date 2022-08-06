using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;

namespace ExemploProjecto3.Operations
{
    public class CustomerOperations : IOperations
    {
        public static CustomerOperations Instance { get; set; }

        static CustomerOperations()
        {
            Instance = new CustomerOperations();
        }

        private CustomerOperations() { }

        public void Add()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                Customer customerToAdd = new Customer
                {
                    CustomerId = "1",
                    CompanyName = "Marta 123",
                    ContactName = "Marta Viegas"
                };
                context.Customers.Add(customerToAdd);
                context.SaveChanges();
            }
        }

        public void Update()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                Customer customerToUpdate = new Customer
                {
                    CustomerId = "1",
                    CompanyName = "Marta 1234",
                    ContactName = "Marta Viegas 2"
                };
                context.Customers.Update(customerToUpdate); // Actualiza por chave primária
                context.SaveChanges();
            }
        }

        public void ShowAll()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                foreach (Customer c in context.Customers)
                {
                    Console.WriteLine(c);
                }
            }
        }

        public void Remove()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                string idToRemove = Console.ReadLine();
                Customer customerToRemove = new Customer
                {
                    CustomerId = idToRemove
                };

                context.Customers.Remove(customerToRemove); // Elimina por chave primária
                context.SaveChanges();
            }
        }
    }
}