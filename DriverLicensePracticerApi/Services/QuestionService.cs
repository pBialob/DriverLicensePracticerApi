using AutoMapper;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DriverLicensePracticerApi.Services
{
    public interface IQuestionService
    {
        QuestionDto GetRandomQuestion();
        IEnumerable<QuestionDto> GetAllQuestions();
        public Question GetSpecifiedQuestion(string points, string level, string category);
        public SingleQuestionSolution ResolveSingleQuestion(Answer answer);
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
            var questionBaseSize = _context.Questions.OrderByDescending(i => i.Id).FirstOrDefault().Id;
            var question = _context.Questions.FirstOrDefault(x=>x.Id == RandomId(questionBaseSize));
            if (question == null) throw new Exception("Question not found");
            var questionDto = _mapper.Map<QuestionDto>(question);

            return questionDto;
        }
        public IEnumerable<QuestionDto> GetAllQuestions()
        {
            var questions = _context.Questions.ToList();
            if (questions == null) throw new Exception("Question hasn't been found");
            var questionsDto = _mapper.Map<List<QuestionDto>>(questions);

            return questionsDto;
        }
        
        public Question GetSpecifiedQuestion(string points, string level, string category)
        {
            var questions = _context.QuestionCategories.Where(x=>(x.Category.Name == category) && (x.Question.QuestionLevel == level) && (x.Question.Points == points)).ToList();
            var question = _context.Questions.FirstOrDefault(x=>x.Id == questions[RandomId(questions.Count)].QuestionId);
            if (question == null) throw new Exception("Question not found");

            return question;
        }

        public SingleQuestionSolution ResolveSingleQuestion(Answer answer)
        {
            var question = _context.Questions.FirstOrDefault(x => x.QuestionNumber == answer.QuestionNumber);
            if (question == null) throw new Exception("Question not found");
            if (answer.GivenAnswer == question.CorrectAnswer) answer.Result = true;
            answer.CorrectAnswer = question.CorrectAnswer;

            var solution = new SingleQuestionSolution()
            {
                Question = _mapper.Map<QuestionDto>(question),
                Answer = _mapper.Map<AnswerDto>(answer)
            };

            return solution;
        }

        private int RandomId(int range)
        {
            var random = new Random();
            int id = random.Next(range);

            return id;
        }
    }
}
