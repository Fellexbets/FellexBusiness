using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace QuizzalT_API.Models
{
    public partial class QuizzalTContext : DbContext
    {
        public QuizzalTContext()
        {
        }

        public QuizzalTContext(DbContextOptions<QuizzalTContext> options) : base(options)
        {
        }

        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<PlayedQuestion> PlayedQuestions { get; set; }
        public virtual DbSet<PlayedQuizz> PlayedQuizzes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Quizz> Quizzes { get; set; }
        public virtual DbSet<QuizzQuestion> QuizzQuestions { get; set; }
        public virtual DbSet<Relation> Relations { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRelation> UserRelations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("QuizzalTContext"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.HasKey(e => new { e.ThemeId, e.UserId });
                entity.Property(e => e.GainedPoints).HasDefaultValueSql("((0))");
                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Achievements)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Achievements_Themes");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Achievements)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Achievements_Users");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.AnswerText)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.DateEdited).HasColumnType("datetime");
                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_Questions");
            });

            modelBuilder.Entity<PlayedQuestion>(entity =>
            {
                entity.Property(e => e.DateAnswered)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.HasOne(d => d.Question)
                    .WithMany(p => p.PlayedQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayedQuestions_Questions");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.PlayedQuestions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayedQuestions_Users");
            });

            modelBuilder.Entity<PlayedQuizz>(entity =>
            {
                entity.HasKey(e => e.PlayedQuizzId);
                entity.Property(e => e.DateAnswered)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.HasOne(d => d.Quizz)
                    .WithMany(p => p.PlayedQuizzes)
                    .HasForeignKey(d => d.QuizzId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayedQuizzes_Quizzes");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.PlayedQuizzes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayedQuizzes_Users");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.DateEdited).HasColumnType("datetime");
                entity.Property(e => e.Difficulty).HasDefaultValueSql("((1))");
                entity.Property(e => e.QuestionName)
                    .IsRequired()
                    .HasMaxLength(30);
                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(15);
                entity.Property(e => e.QuestionImage).HasColumnName("QuestionImage");
                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_Themes");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_Users");
            });

            modelBuilder.Entity<Quizz>(entity =>
            {
                entity.HasKey(e => e.QuizzId);
                entity.Property(e => e.QuizzName)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(15);
                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.DateEdited).HasColumnType("datetime");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quizzes_Users");
            });

            modelBuilder.Entity<QuizzQuestion>(entity =>
            {
                entity.HasKey(e => new { e.QuizzId, e.QuestionId });
                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuizzQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuizzQuestions_Questions");
                entity.HasOne(d => d.Quizz)
                    .WithMany(p => p.QuizzQuestions)
                    .HasForeignKey(d => d.QuizzId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuizzQuestions_Quizzes");
            });

            modelBuilder.Entity<Relation>(entity =>
            {
                entity.Property(e => e.RelationName)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.HasIndex(e => e.ThemeName, "ThemeName");
                entity.HasIndex(e => e.ThemeName, "UQ_Themes_ThemeName")
                    .IsUnique();
                entity.Property(e => e.ThemeName)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "Email");
                entity.HasIndex(e => e.Password, "Password");
                entity.HasIndex(e => e.Email, "UQ_Users_Email")
                    .IsUnique();
                entity.HasIndex(e => e.Username, "UQ_Users_Username")
                    .IsUnique();
                entity.HasIndex(e => e.Username, "Username");
                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.PasswordSalt).HasMaxLength(100);
                entity.Property(e => e.Photo).HasColumnName("Photo");
                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<UserRelation>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RelatedUserId });
                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.HasOne(d => d.RelatedUser)
                    .WithMany(p => p.UserRelationRelatedUsers)
                    .HasForeignKey(d => d.RelatedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK2_UserRelations_Users");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRelationUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK1_UserRelations_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
