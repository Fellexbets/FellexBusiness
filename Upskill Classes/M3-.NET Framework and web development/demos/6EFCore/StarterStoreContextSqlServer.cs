using System;

// dotnet add Microsoft.EntityFrameworkCore.SqlServer
using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    public class StarterStoreContextSqlServer : DbContext 
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder) { 
            // optionsBuilder.UseSqlServer("Server=.;Database=Northwind2016;User Id=sa;Password=tagus;");
            optionsBuilder.UseSqlServer("Server=.;Database=Northwind;User Id=sa;Password=UpSkill!12;");
        }       
    }
}