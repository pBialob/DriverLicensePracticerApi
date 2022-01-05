using DriverLicensePracticerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DriverLicensePracticerApi.Entities
{
    public class ApplicationDbContext : DbContext
    {
        private string _connectionString = "Server=.;Database=DriverLicensePracticerDb;Trusted_Connection=True;";

        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        
    }

}
