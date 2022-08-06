using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // ILoggerFactory
using Microsoft.EntityFrameworkCore.Infrastructure; // db.GetService<>
using System.Collections.Generic;
using System.Linq;

namespace EFCore
{
    public class Program
    {
        static void Main(string[] args)
        {
            // SimpleEFCoreExample();
            // QueryingStockAndProduct();
            // QueryingTop5Products();
            ChangeProduct();
        }

        private static void ChangeProduct()
        {
            using (NorthwindContext db = new NorthwindContext())
            {
                // Update
                db.GetService<ILoggerFactory>().AddProvider(new ConsoleLoggerProvider());
                db.Products.Find(1).ProductName = "Produto Upskill";
                db.SaveChanges();

                // Remove
                db.Products.Remove(db.Products.Find(2));
                db.SaveChanges();
            }
        }

        private static void QueryingTop5Products()
        {
            using (NorthwindContext db = new NorthwindContext())
            {
                Console.WriteLine("Listing only top 5 most expensive products");
                IQueryable<Product> products = db.Products
                                                 .AsNoTracking()
                                                 .OrderByDescending(product => product.Cost)
                                                 .Take(5);

                foreach (Product product in products)
                {
                    Console.WriteLine(product);
                }
            }
        }

        private static void QueryingStockAndProduct()
        {
            using (NorthwindContext db = new NorthwindContext())
            {
                Console.WriteLine("Listing Products and Stock above 100:");
                var products = db.Products
                                           .AsNoTracking()
                                           .Where(product => product.Stock > 100)
                                           .Select(product => new { product.ProductName, product.Stock })
                                           .ToList();

                foreach(var product in products)
                { 
                    Console.WriteLine($"{product.ProductName} {product.Stock}");
                }
            }
        }

        private static void SimpleEFCoreExample() 
        {
            // 1. dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            // 2. using Microsoft.EntityFrameworkCore;
            using (NorthwindContext db = new NorthwindContext())
            {
                db.GetService<ILoggerFactory>().AddProvider(new ConsoleLoggerProvider());

                foreach (Product p in db.Products)
                {
                    Console.WriteLine(p);
                }
            }
        }
    }
}
