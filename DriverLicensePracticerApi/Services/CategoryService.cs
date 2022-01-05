using DriverLicensePracticerApi.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DriverLicensePracticerApi.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void setupCategories(string categoriesToSplit, int questionId)
        {
            var question = _dbContext.Questions.Include(x => x.QuestionCategories).FirstOrDefault(x => x.Id == questionId);
            var splittedCategories = categoriesToSplit.Split(',').ToList();
            foreach (var category in splittedCategories)
            {
                var categoryToAdd = _dbContext.Categories.Include(x => x.QuestionCategories).Where(x => x.Name == category).FirstOrDefault();
                var questionCategory = new QuestionCategory
                {
                    Category = categoryToAdd,
                    CategoryId = categoryToAdd.Id,
                    Question = question,
                    QuestionId = question.Id
                };
                question.QuestionCategories.Add(questionCategory);
                _dbContext.SaveChanges();
            }
        }
    }
}
