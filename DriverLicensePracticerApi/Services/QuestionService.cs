using AutoMapper;
using DriverLicensePracticerApi.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DriverLicensePracticerApi.Services
{
    public interface IQuestionService
    {
        Question GetRandomQuestion();
        IEnumerable<Question> GetAllQuestions();
        public Question GetSpecifiedQuestion(string points, string level, string category);
    }

    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationMappingProfile _appMappingProfile;
        public QuestionService(ApplicationDbContext context, IMapper mapper, ApplicationMappingProfile applicationMappingProfile)
        {
            _context = context;
            _mapper = mapper;
            _appMappingProfile = applicationMappingProfile;
        }
        public Question GetRandomQuestion()
        {   
            var questionBaseSize = _context.Questions.OrderByDescending(i => i.Id).FirstOrDefault().Id;
            var question = _context.Questions.FirstOrDefault(x=>x.Id == RandomId(questionBaseSize));

            return question;
        }
        public IEnumerable<Question> GetAllQuestions()
        {
            var questions = _context.Questions.ToList();
            if (questions == null) throw new Exception("Question hasn't been found");

            return questions;
        }
        
        public Question GetSpecifiedQuestion(string points, string level, string category)
        {
            var questions = _context.QuestionCategories.Where(x=>(x.Category.Name == category) && (x.Question.QuestionLevel == level) && (x.Question.Points == points)).ToList();
            var question = _context.Questions.FirstOrDefault(x=>x.Id == questions[RandomId(questions.Count)].QuestionId);

            return question;
        }

        private int RandomId(int range)
        {
            var random = new Random();
            int id = random.Next(range);

            return id;
        }
    }
}
