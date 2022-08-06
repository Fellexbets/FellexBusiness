using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // ILoggerFactory
using Microsoft.EntityFrameworkCore.Infrastructure; // db.GetService<>
using System.Collections.Generic;
using System.Linq;
using EFCore.Models;

namespace EFCore
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 1. dotnet tool install --global dotnet-ef
            // 2. dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            // 3. dotnet add package Microsoft.EntityFrameworkCore.Design
            // 4. compilar projecto
            // 5. dotnet-ef dbcontext scaffold "Server=.;Database=Northwind;User Id=sa;Password=upskill;" Microsoft.EntityFrameworkCore.SqlServer - o Models
            using (NorthwindContext db = new NorthwindContext())
            {
                
            }
        }
    }
}
