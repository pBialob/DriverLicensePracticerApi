using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DriverLicensePracticerApi.Entities
{
    public class ApplicationDbContext : DbContext
    {
        private string _connectionString = "Server=.;Database=DriverLicensePracticerDb;Trusted_Connection=True;";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Answer> Answers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<QuestionCategory>().HasKey(sc => new { sc.CategoryId, sc.QuestionId });

            modelBuilder.Entity<QuestionCategory>()
                .HasOne<Question>(sc => sc.Question)
                .WithMany(s => s.QuestionCategories)
                .HasForeignKey(sc => sc.QuestionId);


            modelBuilder.Entity<QuestionCategory>()
                .HasOne<Category>(sc => sc.Category)
                .WithMany(s => s.QuestionCategories)
                .HasForeignKey(sc => sc.CategoryId);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();

            modelBuilder.Entity<Test>()
                .HasOne(t => t.User);

            modelBuilder.Entity<Test>()
                .HasMany(q => q.Questions)
                .WithMany(t => t.Tests);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Tests)
                .WithMany(t => t.Questions);

            modelBuilder.Entity<Answer>()
                .HasOne(t => t.Test);

            modelBuilder.Entity<Test>()
                .HasMany(t => t.Answers)
                .WithOne(a => a.Test);


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.EnableSensitiveDataLogging();
        
        }
        
    }

}
