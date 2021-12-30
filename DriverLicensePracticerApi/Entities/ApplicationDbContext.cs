using DriverLicensePracticerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DriverLicensePracticerApi.Entities
{
    public class ApplicationDbContext : DbContext
    {
        private string _connectionString = "Server=.;Database=DriverLicensePracticerDb;Trusted_Connection=True;";

        public DbSet<Question> Questions { get; set; }
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

}
