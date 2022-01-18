using DriverLicensePracticerApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DriverLicensePracticerApi.Repositories
{
    public interface IQuestionRepository
    {
        Question GetRandomQuestion();
        public List<Question> GetAllQuestions();
        public Question GetSpecifiedQuestion(string points, string level, string category);
        public List<Question> GetSpecifiedQuestions(string points, string level, string category, int count);
        public Question GetQuestionByNumber(string number);
        public List<Question> GetTestQuestions(int testId);
        public void SetupQuestionCategories(string categoryNames, int questionId);
        void Save();
    }

    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryRepository _categoryRepository;

        public QuestionRepository(ApplicationDbContext context, ICategoryRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Question GetRandomQuestion()
        {
            var question = _context.Questions
                .Include(q => q.QuestionCategories)
                .OrderBy(q => Guid.NewGuid())
                .Take(1)
                .First();

            if (question == null) throw new Exception("Question not found");

            return question;
        }

        public List<Question> GetAllQuestions()
        {
            var questions = _context.Questions.ToList();

            if (questions == null) throw new Exception("Questions not found");

            return questions;
        }

        public Question GetSpecifiedQuestion(string points, string level, string category)
        {
            var question = _context.Questions
                .Include(c => c.QuestionCategories)
                .Where(q => q.QuestionCategories.Any(qc => qc.Category.Name == category) && (q.Points == points) && (q.QuestionLevel == level))
                .OrderBy(qs => Guid.NewGuid())
                .Take(1)
                .First();    
            
            if (question == null) throw new Exception("Question not found");

            return question;

        }
        public List<Question> GetTestQuestions(int testId)
        {
            var questions = _context.Tests
                .Include(q => q.Questions)
                .Where(x => x.Id == testId)
                .SelectMany(c => c.Questions)
                .ToList();

            if (questions == null) throw new Exception("Questions not found");
            
            return questions;
        }
        public List<Question> GetSpecifiedQuestions(string points, string level, string category, int count)
        {
            var questions = _context.Questions
                .Include(c => c.QuestionCategories)
                .Where(q => q.QuestionCategories.Any(qc => qc.Category.Name == category) && (q.Points == points) && (q.QuestionLevel == level))
                .OrderBy(qs => Guid.NewGuid())
                .Take(count).ToList();

            if (questions == null) throw new Exception("Questions not found");

            return questions;
        }

        public Question GetQuestionByNumber(string number)
        {
            var question = _context.Questions
                .FirstOrDefault(x => x.QuestionNumber == number);

            if (question == null) throw new Exception("Question not found");

            return question;
        }
       
        public void SetupQuestionCategories(string categoryNames, int questionId)
        {
            var question = _context.Questions
                .FirstOrDefault(q=>q.Id == questionId);
            var names = categoryNames.Split();

            foreach (var name in names)
            {
                var category = _categoryRepository.GetCategoryByName(name);
                var questionCategory = new QuestionCategory
                {
                    Category = category,
                    CategoryId = category.Id,
                    Question = question,
                    QuestionId = question.Id
                };
                question.QuestionCategories.Add(questionCategory);
            }

            Save();
        }

    }
}
