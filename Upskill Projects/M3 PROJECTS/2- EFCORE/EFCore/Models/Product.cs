using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace EFCore.Models
{
    public partial class Product : Entity
    {
        public Product()
        {
            
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        
        public virtual Supplier Supplier { get; set; }


        public override void Create()
        {
            using (NorthwindContext db = new NorthwindContext())
            {
                Product product1 = new Product();
                Console.WriteLine("Write the product's name");
                product1.ProductName = Console.ReadLine();
                Console.WriteLine("write the product's price");
                product1.UnitPrice = decimal.Parse(Console.ReadLine());
                Console.WriteLine("write the product's price");
                product1.UnitsInStock = short.Parse(Console.ReadLine());
                db.Add(product1);
                db.SaveChanges();
                Console.WriteLine("Your iten was created! :)");
            }
        }

        public override void Update()
        {
            using (NorthwindContext db = new NorthwindContext())
            {

            Console.WriteLine("Write the ID of the product you want to update");
            int productToUpdate = int.Parse(Console.ReadLine());
            Console.WriteLine("What is the new product name?");
            db.Products.Find(productToUpdate).ProductName = Console.ReadLine();
            Console.WriteLine("What is the new quantity per unit?");
            db.Products.Find(productToUpdate).QuantityPerUnit = Console.ReadLine();
            Console.WriteLine("What is the new unit price?");
            db.Products.Find(productToUpdate).UnitPrice = decimal.Parse(Console.ReadLine());
            Console.WriteLine("What is the new quantity in stock?");
            db.Products.Find(productToUpdate).UnitsInStock = short.Parse(Console.ReadLine());
            Console.WriteLine("What is the new quantity of units on order?");
            db.Products.Find(productToUpdate).UnitsOnOrder = short.Parse(Console.ReadLine());
            Console.WriteLine("What is the new reorder level?");
            db.Products.Find(productToUpdate).ReorderLevel = short.Parse(Console.ReadLine());
            db.SaveChanges();
            Console.WriteLine("Your changes were saved! :)");
            }
        }

        public override void RemoveIten()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            using (NorthwindContext db = new NorthwindContext())
            {

                Console.WriteLine("Select the ID of the Product you want to remove");
                int productToRemove = int.Parse(Console.ReadLine());
                db.Products.Remove(db.Products.Find(productToRemove));
                db.SaveChanges();
                Console.WriteLine("Your iten was removed! :)");

            }
        }

        public override void ListAll()
        {
            Console.ForegroundColor = ConsoleColor.White;
            using (NorthwindContext db = new NorthwindContext())
            {
                var products = db.Products
                                           .AsNoTracking()
                                          .Select(product => new { product.ProductName, product.UnitPrice, product.UnitsInStock })
                                          .ToList();

                foreach (var product in products)
                {
                    Console.WriteLine($"The best product we have is {product.ProductName}, priced at {product.UnitPrice}US$. We have {product.UnitsInStock} of those in stock.");
                }
            }
        }





    }
}
