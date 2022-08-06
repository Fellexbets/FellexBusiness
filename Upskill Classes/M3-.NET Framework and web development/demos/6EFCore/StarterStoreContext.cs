using System;
using Microsoft.EntityFrameworkCore;

namespace EFCore{
    // this manages the connection to the database 
    public class StarterStoreContext : DbContext {

        // these properties map to tables in the database
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder) { 
            string path = System.IO.Path.Combine( System.Environment.CurrentDirectory, "db/StarterStore.db");
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder) { 
        }
    }
}