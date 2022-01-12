using DriverLicensePracticerApi.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DriverLicensePracticerApi.Repositories
{
    public interface IQuestionRepository
    {
        Question GetRandomQuestion();
        public List<Question> GetAllQuestions();
        public Question GetSpecifiedQuestion(string points, string level, string category);
        public Question GetQuestionByNumber(string number);
        void Save();
    }

    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Question GetRandomQuestion()
        {
            var question = _context.Questions
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

        public Question GetQuestionByNumber(string number)
        {
            var question = _context.Questions
                .FirstOrDefault(x => x.QuestionNumber == number);

            if (question == null) throw new Exception("Question not found");

            return question;
        }

    }
}
