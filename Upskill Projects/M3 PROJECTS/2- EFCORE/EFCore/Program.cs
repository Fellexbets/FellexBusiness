using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // ILoggerFactory
using Microsoft.EntityFrameworkCore.Infrastructure; // db.GetService<>
using System.Collections.Generic;
using System.Linq;
using EFCore.Models;
using System.Text;

namespace EFCore
{
    public class Program
    {

        public static bool EndProgram { get; set; } = false;
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀");
            Console.WriteLine("💀💀💀    C# PROJECT - v2.0 -  💀💀💀");
            Console.WriteLine("💀💀💀  EFCORE - 31-03-2022    💀💀💀");
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

            using (NorthwindContext db = new NorthwindContext())
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
