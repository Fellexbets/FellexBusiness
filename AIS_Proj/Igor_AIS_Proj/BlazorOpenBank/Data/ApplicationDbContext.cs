
using Igor_AIS_Proj.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorOpenBank.Data
{
    public class BlazorContext : DbContext
    {
        public BlazorContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        //public PostgresContext(DbContextOptions<PostgresContext> options)
        //    : base(options)
        //{
        //}

        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Movement> Movements { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("AISProject"))
                    .EnableSensitiveDataLogging();
            }
        }
    }
}
