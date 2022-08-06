using System;
using Microsoft.EntityFrameworkCore; 
using System.Linq;

using Microsoft.Extensions.Logging; //ILoggerFactory
using Microsoft.EntityFrameworkCore.Infrastructure; //db.GetService<>()
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryingProductsAndStock();
            // QueryingTop5Products();
            // ChangeProduct();
        }

        static void ChangeProduct()
        {
            using (var db = new StarterStoreContextSqlServer()) 
            { 
                db.Products.Find(1).ProductName = "Chai";
                db.SaveChanges();

                // Microsoft.EntityFrameworkCore.DbUpdateException
                db.Products.Remove(db.Products.Find(2));
                db.SaveChanges();
            }
        }

        static void QueryingProductsAndStock() {
            using (var db = new StarterStoreContextSqlServer()) 
            {
                // db.GetService<ILoggerFactory>().AddProvider(new ConsoleLoggerProvider());

                Console.WriteLine("Listing products and stock:");

                List<Product> products = db.Products
                                            .AsNoTracking()
                                            .Where(product => product.Stock > 100 )
                                            .OrderBy(product => product.ProductName)
                                            .ToList();

                foreach (Product p in products) 
                { 
                    Console.WriteLine($"{p.ProductName} has {p.Stock} units left."); 
                } 
            }
        }

        static void QueryingTop5Products(){
            using (var db = new StarterStoreContextSqlServer()) 
            { 
                // db.GetService<ILoggerFactory>().AddProvider(new ConsoleLoggerProvider());
                
                Console.WriteLine("Listing Only 5 products:");

                IQueryable<Product> products = db.Products
                                                 .Where(product => product.Stock > 100) 
                                                 .OrderByDescending(product => product.ProductName)
                                                 .Take(5);

                foreach (Product p in products) { 
                    Console.WriteLine($"{p.ProductName}"); 
                } 

            }
        }
    }
}
