using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
#nullable disable

namespace EFCore.Models
{
    public partial class Shipper : Entity
    {
        public Shipper()
        {
            
        }

        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public override void Create()
        {
            using (NorthwindContext db = new NorthwindContext())
            {
                Shipper shipper1 = new Shipper();
                Console.WriteLine("Write the shipper's Company name");
                shipper1.CompanyName = Console.ReadLine();
                Console.WriteLine("Write the shipper's phone");
                shipper1.Phone = Console.ReadLine();
                db.Add(shipper1);
                db.SaveChanges();
                Console.WriteLine("Your iten was created! :)");
            }
        }

        public override void Update()
        {
            Console.ForegroundColor = ConsoleColor.White;
            using (NorthwindContext db = new NorthwindContext())
            {
                Console.WriteLine("Write the ID of the shipper you want to update");
                int shipperToUpdate = int.Parse(Console.ReadLine());
                Console.WriteLine("What is the new shipper's company name?");
                db.Shippers.Find(shipperToUpdate).CompanyName = Console.ReadLine();
                Console.WriteLine("What is the new shipper's phone?");
                db.Shippers.Find(shipperToUpdate).Phone = Console.ReadLine();
                db.SaveChanges();
                Console.WriteLine("Your changes were saved! :)");


            }
        }

        public override void RemoveIten()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            using (NorthwindContext db = new NorthwindContext())
            {

                Console.WriteLine("Select the ID of the Shipper you want to remove");
                int shipperToRemove = int.Parse(Console.ReadLine());
                db.Shippers.Remove(db.Shippers.Find(shipperToRemove));
                db.SaveChanges();
                Console.WriteLine("Your iten was removed! :)");

            }
        }

        public override void ListAll()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            using (NorthwindContext db = new NorthwindContext())
            {
                var shippers = db.Shippers
                                           .AsNoTracking()
                                          .Select(shipper => new { shipper.CompanyName, shipper.Phone })
                                          .ToList();

                foreach (var shipper in shippers)
                {
                    Console.WriteLine($"Our favorite shipper is named {shipper.CompanyName}. They can be contacted at {shipper.Phone}.");
                }
            }
        }


    }
}
