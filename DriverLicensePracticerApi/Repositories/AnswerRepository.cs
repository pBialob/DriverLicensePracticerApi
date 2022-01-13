using DriverLicensePracticerApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DriverLicensePracticerApi.Repositories
{
    public interface IAnswerRepository
    {
        void Save();
        public List<Answer> GetTestAnswers(int testId);
        public void AddMany(List<Answer> answers);
        public void Add(Answer answer);
    }

    public class AnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext _context;
        public AnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Answer answer)
        {
            _context.Answers.Add(answer);
        }
        public void AddMany(List<Answer> answers)
        {
            _context.AddRange(answers);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public List<Answer> GetTestAnswers(int testId)
        {
            var answers = _context.Answers.Where(x=>x.TestId == testId).ToList();
            if (answers == null) throw new Exception("Answers not found");

            return answers;
        }
    }
}
