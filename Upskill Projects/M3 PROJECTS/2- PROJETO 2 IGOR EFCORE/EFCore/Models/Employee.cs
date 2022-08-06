using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace EFCore.Models
{
    public partial class Employee : Entity
    {
        public Employee()
        {
           
        }

        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
      

     


        public override void Create()
        {
            using (NorthwindContext db = new NorthwindContext())
            {
                Employee employee1 = new Employee();
                Console.WriteLine("Write the Employee's Last Name");
                employee1.LastName = Console.ReadLine();
                Console.WriteLine("write the Employee's First Name");
                employee1.FirstName = Console.ReadLine();
                Console.WriteLine("write the Employee's Title");
                employee1.Title = Console.ReadLine();
                Console.WriteLine("write the Employee's Adress");
                employee1.Address = Console.ReadLine();
                Console.WriteLine("write the customers Country");
                employee1.Country = Console.ReadLine();
                db.Add(employee1);
                db.SaveChanges();
                Console.WriteLine("Your iten was created! :)");
            }
        }

        public override void Update()
        {
            Console.ForegroundColor = ConsoleColor.White;
            using (NorthwindContext db = new NorthwindContext())
            {


                Console.WriteLine("Write the ID of the employee you want to update");
                int employeeToUpdate = int.Parse(Console.ReadLine());
                Console.WriteLine("What is the new employee's Last Name?");
                db.Employees.Find(employeeToUpdate).LastName = Console.ReadLine();
                Console.WriteLine("What is the new employee's First Name?");
                db.Employees.Find(employeeToUpdate).FirstName = Console.ReadLine();
                Console.WriteLine("What is the new employee's title?");
                db.Employees.Find(employeeToUpdate).Title = Console.ReadLine();
                Console.WriteLine("What is the new employee's Title of Courtesy?");
                db.Employees.Find(employeeToUpdate).TitleOfCourtesy = Console.ReadLine();
                Console.WriteLine("What is the new employee's birth Date?");
                db.Employees.Find(employeeToUpdate).BirthDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("What is the new employee's hire date?");
                db.Employees.Find(employeeToUpdate).HireDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("What is the new employee's address?");
                db.Employees.Find(employeeToUpdate).Address = Console.ReadLine();
                Console.WriteLine("What is the new employee's city?");
                db.Employees.Find(employeeToUpdate).City = Console.ReadLine();
                Console.WriteLine("What is the new employee's postal code?");
                db.Employees.Find(employeeToUpdate).PostalCode = Console.ReadLine();
                Console.WriteLine("What is the new employee's country?");
                db.Employees.Find(employeeToUpdate).Country = Console.ReadLine();
                Console.WriteLine("What is the new customers phone?");
                db.Employees.Find(employeeToUpdate).HomePhone = Console.ReadLine();
                db.SaveChanges();
                Console.WriteLine("Your changes were saved! :)");
            }
        }

        public override void RemoveIten()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            using (NorthwindContext db = new NorthwindContext())
            {

                Console.WriteLine("Select the ID of the Employee you want to remove");
                int employeeToRemove = int.Parse(Console.ReadLine());
                db.Employees.Remove(db.Employees.Find(employeeToRemove));
                db.SaveChanges();
                Console.WriteLine("Your iten was removed! :)");

            }
        }

        public override void ListAll()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            using (NorthwindContext db = new NorthwindContext())
            {
                var employees = db.Employees
                                           .AsNoTracking()
                                          .Select(employee => new { employee.FirstName, employee.LastName, employee.Title, employee.HireDate })
                                          .ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine($"Our most faithful employee {employee.FirstName} {employee.LastName} currently works as {employee.Title}. They were hired on {employee.HireDate}.");
                }
            }
        }



    }
}
