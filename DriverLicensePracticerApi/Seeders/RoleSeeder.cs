using DriverLicensePracticerApi.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DriverLicensePracticerApi.Seeders
{
    public class RoleSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        public RoleSeeder(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }
        }

        public List<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role() { Name = "User"}
            };
            return roles;
        }
    }
}
