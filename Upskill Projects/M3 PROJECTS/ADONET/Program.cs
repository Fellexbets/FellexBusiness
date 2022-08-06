using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // ILoggerFactory
using Microsoft.EntityFrameworkCore.Infrastructure; // db.GetService<>
using System.Collections.Generic;
using System.Linq;
using EFCore.Models;
using System.Text;
using Microsoft.Data.SqlClient;

namespace EFCore
{
    public class Program
    {
        public static string connectionString = $"Server=.;Database=Northwind;User Id=sa;Password=raga123;TrustServerCertificate=true;";
        public static bool EndProgram { get; set; } = false;
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀");
            Console.WriteLine("💀💀💀    C# PROJECT - v2.0    💀💀💀");
            Console.WriteLine("💀💀💀    ADO.NET 07-04-2022   💀💀💀");
            Console.WriteLine("💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀");


            Console.WriteLine("                                    _______          ");
            Console.WriteLine("                                   /       \\        ");
            Console.WriteLine("                               __ /   .-.  .\\        ");
            Console.WriteLine("                              /  `\\  /   \\/  \\       ");
            Console.WriteLine("                              | _ \\/   .==.==.\\           ");
            Console.WriteLine("                              |                 \\          ");
            Console.WriteLine("                              | (   \\ / ____\\__\\             ");
            Console.WriteLine("                              \\      (_()(_()                  ");
            Console.WriteLine("                               \\            '---.___             ");
            Console.WriteLine("                               \\                    \\               ");
            Console.WriteLine("                            /\\ |`       (__)________//                ");
            Console.WriteLine("                           /  \\|      /\\___/                 ");
            Console.WriteLine("                          |    \\     \\||VVV                  ");
            Console.WriteLine("                          |     \\    \\|_____                 ");
            Console.WriteLine("                          |      \\     ______)                   ");
            Console.WriteLine("                          \\       \\  /`                          ");
            Console.WriteLine("                                   \\(                                ");

           
            
            {
                do
                {
                    ProgramOperations.ChooseOperation();


                }
                while (!EndProgram);
            }
        }
    }
}
