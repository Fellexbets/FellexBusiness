using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RoadToDB
{
    public class FileUtils
    {
        public static async void WriteCustomersToHtml()
        {
            // Asynchronous programming: https://docs.microsoft.com/en-us/dotnet/csharp/async
            List<string> lines = new List<string>
            {
                "<html><head></head><body><h1>Customers' List</h1>"
            };
            foreach (Customer customer in Manager<Customer>.Instance)
            {
                lines.Add($"<div><h2>{customer.ContactTitle} {customer.ContactName}</h2>");
                lines.Add($"<h3>{customer.Address}, {customer.City}, {customer.Country}</h3></div>");
            }
            lines.Add("</body></html>");
            await File.WriteAllLinesAsync(@"c:\inetpub\wwwroot\customers.html", lines, Encoding.UTF8);
        }
    }
}
