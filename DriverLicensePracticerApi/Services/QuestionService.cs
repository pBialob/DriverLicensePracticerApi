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
        public QuestionDto GetSpecifiedQuestion(string points, string level, string category);
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
        public QuestionDto GetRandomQuestion()
        {   
            var questionBaseSize = _context.Questions.OrderByDescending(i => i.Id).FirstOrDefault().Id;
            var question = _context.Questions.FirstOrDefault(x=>x.Id == RandomId(questionBaseSize));
            var questionDto = MapQuestion(question);

            return questionDto;
        }
        public IEnumerable<QuestionDto> GetAllQuestions()
        {
            var questions = _context.Questions.ToList();
            var result = _mapper.Map<List<QuestionDto>>(questions);

            return result;
        }
        
        public QuestionDto GetSpecifiedQuestion(string points, string level, string category)
        {
            var questions = _context.QuestionCategories.Where(x=>(x.Category.Name == category) && (x.Question.QuestionLevel == level) && (x.Question.Points == points)).ToList();
            var questionDto = MapQuestion(_context.Questions.Where(x=>x.Id == questions[RandomId(questions.Count)].QuestionId).FirstOrDefault());
            return questionDto;
        }

        private QuestionDto MapQuestion(Question question)
        {
            var questionCategories = _context.QuestionCategories.Where(x=>x.QuestionId == question.Id).ToList();
            var questionCategoriesDto = _mapper.Map<List<CategoryDto>>(questionCategories);
            var questionDto = _mapper.Map<QuestionDto>(question);
            questionDto.Categories = questionCategoriesDto;
            _appMappingProfile.MapCategoryName(questionDto);

            return questionDto;
        }

        private int RandomId(int range)
        {
            var random = new Random();
            int id = random.Next(range);

            return id;
        }
    }
}
