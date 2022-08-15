using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Transfer> Transfers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

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

                entity.Property(e => e.Accountid)
                    .HasColumnName("accountid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Balance)
                    .HasPrecision(10)
                    .HasColumnName("balance");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Currency)
                    .HasMaxLength(15)
                    .HasColumnName("currency");

                entity.Property(e => e.Userid).HasColumnName("userid");

            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.HasKey(e => new { e.Transferid, e.Originaccountid, e.Destinationaccountid })
                    .HasName("transfer_pk");

                entity.ToTable("transfers");

                entity.Property(e => e.Transferid).HasColumnName("transferid");

                entity.Property(e => e.Originaccountid).HasColumnName("originaccountid");

                entity.Property(e => e.Destinationaccountid).HasColumnName("destinationaccountid");

                entity.Property(e => e.Transferamount)
                    .HasPrecision(10)
                    .HasColumnName("transferamount");

                entity.Property(e => e.Transfertime)
                    .HasColumnName("transfertime")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.Destinationaccount)
                    .WithMany(p => p.TransferDestinationaccounts)
                    .HasForeignKey(d => d.Destinationaccountid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("destinationaccountid_fk");

               
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "users_email_key")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "users_username_key")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Username)
                    .HasMaxLength(25)
                    .HasColumnName("username");

                entity.Property(e => e.Userpassword)
                    .HasMaxLength(50)
                    .HasColumnName("userpassword");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(250)
                    .HasColumnName("passwordsalt");

                entity.Property(e => e.UserToken)
                    .HasMaxLength(600)
                    .HasColumnName("usertoken");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
