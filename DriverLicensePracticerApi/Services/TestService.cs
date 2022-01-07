using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using DriverLicensePracticerApi.Services.TestGenerator;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System;
using System.Data.Entity;
using AutoMapper;

namespace DriverLicensePracticerApi.Services
{
    public interface ITestService
    {
        TestDto GenerateTestQuestions(string category);
        TestDto SolveTest(List<Answer> answers, int testId);
        TestDto GetSpecifiedTest(int testId);
    }

    public class TestService : ITestService
    {
        private readonly ApplicationDbContext _context;
        private readonly TestFactory _factory;
        private readonly IHttpContextAccessor _http;
        private readonly IMapper _mapper;
        public TestService(ApplicationDbContext context, TestFactory testFactory, IHttpContextAccessor http, IMapper mapper)
        {
            _factory = testFactory;
            _context = context;
            _http = http;
            _mapper = mapper;
        }
        public TestDto GenerateTestQuestions(string category)
        {
            var questions = _factory.GenerateTest(category).GetTest();
            var test = new Test()
            {
                Questions = questions,
                UserId = Int32.Parse(_http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
            };
            _context.Tests.Add(test);
            _context.SaveChanges();

            var testDto = new TestDto()
            {
                TestId = test.Id,
                Questions = _mapper.Map<List<QuestionDto>>(questions)
            };

            return testDto;
        }

        public TestDto SolveTest(List<Answer> answers, int testId)
        {
            var test = _context.Tests.FirstOrDefault(x => x.Id == testId);

            if (test == null) throw new Exception("Test not found");
            if (test.IsResolved) throw new Exception("Test has been already resolved");

            test.Answers = answers;
            test.IsResolved = true;
            foreach (var answer in answers)
            {
                var question = _context.Questions.FirstOrDefault(x => x.QuestionNumber == answer.QuestionNumber);
                if (question == null) throw new Exception("Question not found");
                if(answer.GivenAnswer == question.CorrectAnswer)
                {
                    test.Score += Int32.Parse(question.Points);
                    answer.Result = true;
                }
                answer.CorrectAnswer = question.CorrectAnswer; 
            }

            _context.Answers.AddRange(answers);
            _context.SaveChanges();

            var testDto = new TestDto()
            {
                TestId=test.Id,
                Questions = _mapper.Map<List<QuestionDto>>(GetTestQuestions(answers)),
                Answers = answers,
                Score = test.Score,
                IsResolved = test.IsResolved,
            };

            return testDto;
        }

        public TestDto GetSpecifiedTest(int testId)
        {
            var test = _context.Tests
                .FirstOrDefault(x => x.Id == testId);
            if (test == null) throw new Exception("Test not Found");

            var answers = _context.Answers.Where(x=>x.TestId == testId).ToList();
            var questions = GetTestQuestions(answers);

            var testDto = new TestDto()
            {
                TestId = test.Id,
                Questions = _mapper.Map<List<QuestionDto>>(questions),
                Answers = answers,
                Score = test.Score,
                IsResolved = test.IsResolved
            };

            return testDto; 
        }

        private List<Question> GetTestQuestions(List<Answer> answers)
        {
            var questions = new List<Question>();
            foreach (var answer in answers)
            {
                questions.Add(_context.Questions.FirstOrDefault(x => x.QuestionNumber == answer.QuestionNumber));
            }

            return questions;
        }
    }
}
