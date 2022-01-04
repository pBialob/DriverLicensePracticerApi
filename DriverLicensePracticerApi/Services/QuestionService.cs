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
            Random rnd = new Random();
            var questionBaseSize = _context.Questions.OrderByDescending(i => i.Id).FirstOrDefault().Id;
            var randomId = rnd.Next(1, questionBaseSize);
            var questionCategories = _context.QuestionCategories.Where(x => x.QuestionId == randomId).ToList();
            var question = _context.Questions.FirstOrDefault(x=>x.Id == randomId);
            var questionCategoriesDto = _mapper.Map<List<CategoryDto>>(questionCategories);
            var questionDto = _mapper.Map<QuestionDto>(question);
            questionDto.Categories = questionCategoriesDto;
            _appMappingProfile.MapCategoryName(questionDto);


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
