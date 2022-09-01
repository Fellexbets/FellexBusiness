
namespace Igor_AIS_Proj.Models
{
    public partial class PostgresContext : DbContext
    {
        public PostgresContext()
        {
        }

        public PostgresContext(DbContextOptions<PostgresContext> options)
            : base(options)
        {
        }

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
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("AISProject"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");

                entity.Property(e => e.AccountId)
                    .HasColumnName("accountid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Balance)
                    .HasPrecision(10)
                    .HasColumnName("balance");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .HasColumnName("currency");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.UserId).HasColumnName("userid");

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.Accounts)
                //.HasForeignKey(d => d.UserId)
                //    .HasConstraintName("fk_user");

            });

            modelBuilder.Entity<Movement>(entity =>
            {
                entity.ToTable("movements");

                entity.Property(e => e.MovementId)
                    .HasColumnName("movementid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AccountId).HasColumnName("accountid");

                entity.Property(e => e.Amount)
                    .HasPrecision(10)
                    .HasColumnName("amount");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .HasColumnName("currency")
                    .HasDefaultValueSql("'EUR'::character varying");

                entity.Property(e => e.MovimentTime)
                    .HasColumnName("movimenttime")
                    .HasDefaultValueSql("now()");

                //entity.HasOne(d => d.Account)
                //    .WithMany(p => p.Movements)
                //    .HasForeignKey(d => d.AccountId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("accountid_fk");
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.ToTable("transfers");

                entity.Property(e => e.TransferId)
                    .HasColumnName("transferid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Amount)
                    .HasPrecision(10)
                    .HasColumnName("amount");

                entity.Property(e => e.Currency)
                    .HasMaxLength(15)
                    .HasColumnName("currency");

                entity.Property(e => e.DestinationaccountId).HasColumnName("destinationaccountid");

                entity.Property(e => e.OriginaccountId).HasColumnName("originaccountid");

                entity.Property(e => e.TransferDate)
                    .HasColumnName("transferdate")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "users_email_key")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "users_username_key")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("userid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(250)
                    .HasColumnName("passwordsalt");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Username)
                    .HasMaxLength(25)
                    .HasColumnName("username");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .HasColumnName("userpassword");

                entity.Property(e => e.UserToken)
                    .HasMaxLength(600)
                    .HasColumnName("usertoken");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
