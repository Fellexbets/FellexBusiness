using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // ILoggerFactory
using Microsoft.EntityFrameworkCore.Infrastructure; // db.GetService<>
using Microsoft.Extensions.Configuration;

namespace ExemploProjecto3
{
    public class NorthwindContext : DbContext
    {
        public static IConfigurationRoot ConfigurationRoot { get; private set; } = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

        public static bool DebugEnabled { get; set; } = true;
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees{ get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public NorthwindContext()
        {
            // Sempre que criar um novo contexto verifica se está em modo "Debug"
            if (DebugEnabled)
            { 
                this.GetService<ILoggerFactory>().AddProvider(new ConsoleLoggerProvider());
            }
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationRoot["ConnectionString"]);
        }
    }
}
