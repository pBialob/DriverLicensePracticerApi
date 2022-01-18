using DriverLicensePracticerApi.Entities;
using System;
using System.Linq;

namespace DriverLicensePracticerApi.Repositories
{
    public interface ICategoryRepository
    {
        Category GetCategoryByName(string name);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category GetCategoryByName(string name)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Name == name);
            if (category == null) throw new Exception("Category not found");

            return category;
        }
    }
}
