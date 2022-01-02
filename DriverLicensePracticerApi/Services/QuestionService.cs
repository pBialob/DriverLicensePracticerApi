using AutoMapper;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DriverLicensePracticerApi.Services
{
    public interface IQuestionService
    {
        QuestionDto GetRandomQuestion();
        IEnumerable<QuestionDto> GetAllQuestions();
    }

    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public QuestionService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public QuestionDto GetRandomQuestion()
        {
            Random rnd = new Random();
            var questionBaseSize = _context.Questions.OrderByDescending(i => i.Id).FirstOrDefault().Id;
            var randomId = rnd.Next(1, questionBaseSize);
            var question = _context.Questions.FirstOrDefault(i => i.Id == randomId);
            var questionDto = _mapper.Map<QuestionDto>(question);

            return questionDto;
        }
        public IEnumerable<QuestionDto> GetAllQuestions()
        {
            var questions = _context.Questions.ToList();

            var result = _mapper.Map<List<QuestionDto>>(questions);
            return result;
        }
    }
}
