using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
#nullable disable
namespace EFCore.Models
{
    public partial class Supplier : Entity
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
       
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        
      

        public virtual ICollection<Product> Products { get; set; }

        public override void Create()
        {
            using (NorthwindContext db = new NorthwindContext())
            {
                Supplier supplier1 = new Supplier();
                Console.WriteLine("Write the supplier's name");
                supplier1.ContactName = Console.ReadLine();
                Console.WriteLine("write the supplier's area of work");
                supplier1.CompanyName = Console.ReadLine();
                Console.WriteLine("write the supplier's Contact Title");
                supplier1.ContactTitle = Console.ReadLine();
                Console.WriteLine("write the supplier's Adress");
                supplier1.Address = Console.ReadLine();
                Console.WriteLine("write the supplier's City");
                supplier1.City = Console.ReadLine();
                Console.WriteLine("write the supplier's Country");
                supplier1.Country = Console.ReadLine();
                Console.WriteLine("write the supplier's Phone");
                supplier1.Phone = Console.ReadLine();
                db.Add(supplier1);
                db.SaveChanges();
                Console.WriteLine("Your iten was created! :)");
            }
        }

        public override void Update()
        {
            Console.ForegroundColor = ConsoleColor.White;
            using (NorthwindContext db = new NorthwindContext())
            {

                Console.WriteLine("Write the ID of the supplier you want to update");
                int supplierToUpdate = int.Parse(Console.ReadLine());
                Console.WriteLine("What is the new supplier's company name?");
                db.Suppliers.Find(supplierToUpdate).CompanyName = Console.ReadLine();
                Console.WriteLine("What is the new supplier's contact name?");
                db.Suppliers.Find(supplierToUpdate).ContactName = Console.ReadLine();
                Console.WriteLine("What is the new supplier's contact title?");
                db.Suppliers.Find(supplierToUpdate).ContactTitle = Console.ReadLine();
                Console.WriteLine("What is the new supplier's address?");
                db.Suppliers.Find(supplierToUpdate).Address = Console.ReadLine();
                Console.WriteLine("What is the new supplier'ss city?");
                db.Suppliers.Find(supplierToUpdate).City = Console.ReadLine();
                Console.WriteLine("What is the new supplier's postal code?");
                db.Suppliers.Find(supplierToUpdate).PostalCode = Console.ReadLine();
                Console.WriteLine("What is the new supplier's country?");
                db.Suppliers.Find(supplierToUpdate).Country = Console.ReadLine();
                Console.WriteLine("What is the new supplier's phone?");
                db.Suppliers.Find(supplierToUpdate).Phone = Console.ReadLine();
                db.SaveChanges();
                Console.WriteLine("Your changes were saved! :)");
            }
        }

        public override void RemoveIten()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            using (NorthwindContext db = new NorthwindContext())
            {

                Console.WriteLine("Select the ID of the Supplier you want to remove");
                int supplierToRemove = int.Parse(Console.ReadLine());
                db.Shippers.Remove(db.Shippers.Find(supplierToRemove));
                db.SaveChanges();
                Console.WriteLine("Your iten was removed! :)");

            }
        }

        public override void ListAll()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            using (NorthwindContext db = new NorthwindContext())
            {
                var suppliers = db.Suppliers
                                           .AsNoTracking()
                                          .Select(supplier => new { supplier.CompanyName, supplier.ContactName, supplier.ContactTitle, supplier.Country })
                                          .ToList();

                foreach (var supplier in suppliers)
                {
                    Console.WriteLine($"The supplier we most recommend is named {supplier.CompanyName}, from {supplier.Country}. Talk to {supplier.ContactName}, he is the {supplier.ContactTitle}.");
                }
            }
        }
    }
}
