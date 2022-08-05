using Microsoft.EntityFrameworkCore;

namespace AIS_Project_Igor.Models
{
    public class AisProjectContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql("Host=my_host;Database=postgres;Username=postgres;Password=postgrespw");
    }
}
