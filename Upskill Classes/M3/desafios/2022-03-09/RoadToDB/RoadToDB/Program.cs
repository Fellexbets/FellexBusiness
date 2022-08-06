using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace RoadToDB
{
    // More info: 
    //      Attribute:   https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/attributes/
    //      Foreach:     https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/make-class-foreach-statement
    //      IEnumerable: https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=net-5.0
    //      Generics:    https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/

    public class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer
            {
                CustomerID = "100",
                CompanyName = "Empresa"
            };
            Manager<Customer>.Instance.Add(customer);
            Console.WriteLine(Manager<Customer>.Instance);

            Customer customer1 = new Customer
            {
                CustomerID = "100",
                CompanyName = "Empresa 100"
            };
            Manager<Customer>.Instance.Update(customer1);
            Console.WriteLine(Manager<Customer>.Instance);

            // Manager<Customer>.Instance.Remove("100");
            // Console.WriteLine(Manager<Customer>.Instance);

            // Console.WriteLine(Manager<Category>.Instance);
            // WriteCustomersToHtml();
        }

        private static void WriteCustomersToHtml()
        {
            List<string> lines = new List<string>();
            lines.Add("<html><head></head><body><h1>Customers' List</h1>");
            foreach (Customer customer in Manager<Customer>.Instance)
            {
                lines.Add($"<div><h2>{customer.ContactTitle} {customer.ContactName}</h2>");
                lines.Add($"<h3>{customer.Address}, {customer.City}, {customer.Country}</h3></div>");
            }
            lines.Add("</body></html>");
            File.WriteAllLines(@"c:\inetpub\wwwroot\customers.html", lines, Encoding.UTF8);
        }
    }
}
