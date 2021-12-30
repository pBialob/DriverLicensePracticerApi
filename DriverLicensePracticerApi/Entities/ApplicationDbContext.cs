using DriverLicensePracticerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace RestaurantApi.Entities
{
    public class ApplicationDbContext : DbContext
    {
        private string _connectionString = "Server=.;Database=RestaurantDb;Trusted_Connection=True;";

        public DbSet<Question> Questions { get; set; }
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

}
