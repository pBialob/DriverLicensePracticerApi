using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Services;
using System.Collections.Generic;
using System.Linq;

namespace DriverLicensePracticerApi.Seeders
{
    public class CategorySeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly CategoryService _categoryService;
        public CategorySeeder(ApplicationDbContext dbContext, CategoryService categoryService)
        {
            _dbContext = dbContext;
            _categoryService = categoryService;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Categories.Any())
                {
                    var categories = GetCategories();
                    _dbContext.Categories.AddRange(categories);
                    _dbContext.SaveChanges();
                    var questions = _dbContext.Questions.ToList();
                    SetupCategories(questions);
                }  
            }
        }

        public List<Category> GetCategories()
        {
            var categories = new List<Category>()
            {
                new Category() { Name = "A" },
                new Category() { Name = "B" },
                new Category() { Name = "C" },
                new Category() { Name = "D" },
                new Category() { Name = "T" },
                new Category() { Name = "AM" },
                new Category() { Name = "A1" },
                new Category() { Name = "A2" },
                new Category() { Name = "B1" },
                new Category() { Name = "C1" },
                new Category() { Name = "D1" },
                new Category() { Name = "PT" }
            };
            return categories;
        }
     public void SetupCategories(List<Question> questions)
        {
            foreach (var question in questions)
            {
                _categoryService.setupCategories(question.CategoriesToSet, question.Id);
            }         
        }
    }
}
