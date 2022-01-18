using AutoMapper;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DriverLicensePracticerApi.Repositories
{
    public interface IQuestionRepository
    {
        Question GetRandomQuestion();
        public PagedResult<QuestionDto> GetAllQuestions(QuestionQuery query);
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
        private readonly IMapper _mapper;

        public QuestionRepository(ApplicationDbContext context, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
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

        public PagedResult<QuestionDto> GetAllQuestions(QuestionQuery query)
        {
            var baseQuery = _context.Questions
                .Include(q => q.QuestionCategories)
                .Where(q => query.SearchCategory == null || (q.QuestionCategories.Any(qc => qc.Category.Name == query.SearchCategory)))
                .Where(q => query.SearchPoints == null || (q.Points == query.SearchPoints))
                .Where(q => query.SearchLevel == null || (q.QuestionLevel == query.SearchLevel));

            if(string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Question, object>>>
                {
                    {nameof(Question.QuestionLevel), r => r.QuestionName},
                    {nameof(Question.Points), r => r.Points},
                    {nameof(Question.QuestionNumber), r => r.QuestionNumber}
                };

                var selectedCoulum = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                    baseQuery.OrderBy(selectedCoulum)
                        : baseQuery.OrderByDescending(selectedCoulum);
            }

            var questions = baseQuery
                .Skip(query.PageSize * (query.PageNumber-1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            return new PagedResult<QuestionDto>(_mapper.Map<List<QuestionDto>>(questions), totalItemsCount, query.PageSize, query.PageNumber);
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
