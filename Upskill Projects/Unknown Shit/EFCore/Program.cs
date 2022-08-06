using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (NorthwindContext db = new NorthwindContext())
            {
               // db.GetService<ILoggerFactory>().AddProvider(new ConsoleLoggerProvider());

                foreach(Product p in db.Products)
                {
                    Console.WriteLine(p);
                }



              //  foreach(Categories c in db.Categories)
              //  {
              //      Console.WriteLine(c);
              //  }
            }


        }
    }
}
