using Microsoft.EntityFrameworkCore;
using Project3API.Models;


namespace Project3API.Persistence_DB_
{
    public class SQLiteDB : DbContext
    {
        
        public DbSet<Customer> Customer { get; set; }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Shipper> Shipper { get; set; }

        public DbSet<Supplier> Supplier { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Filename=db/database.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Shipper>().ToTable("Shippers");
            modelBuilder.Entity<Supplier>().ToTable("Suppliers");
        }
    }
}
