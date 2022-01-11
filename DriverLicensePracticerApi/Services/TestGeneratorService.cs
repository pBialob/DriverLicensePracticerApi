using DriverLicensePracticerApi.Entities;
using System.Collections.Generic;


namespace DriverLicensePracticerApi.Services.TestGenerator.Tests
{
    public interface ITestGenerator
    {
        public List<Question> GetTest(string category);
    }
    public class TestGeneratorService : ITestGenerator
    {
        private const string primaryLevel = "PODSTAWOWY";
        private const string specialistLevel = "SPECJALISTYCZNY";
        private readonly IQuestionService _questionService;
        public TestGeneratorService(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public List<Question> GetTest(string category)
        {
            var test = new List<Question>();
            test.AddRange(GetPrimaryPart(_questionService, primaryLevel, category));
            test.AddRange(GetSpecialistPart(_questionService, specialistLevel, category));

            return test;
        }
        private List<Question> GetPrimaryPart(IQuestionService questionService, string level, string category)
        {
            var questions = new List<Question>();
            for (int i = 0; i < 1; i++)
            {
                questions.Add(questionService.GetSpecifiedQuestion("3", level, category));
            }
            for (int i = 0; i < 1; i++)
            {
                questions.Add(questionService.GetSpecifiedQuestion("2", level, category));
            }
            for (int i = 0; i < 1; i++)
            {
                questions.Add(questionService.GetSpecifiedQuestion("1", level, category));
            }

            return questions;
        }
        private List<Question> GetSpecialistPart(IQuestionService questionService, string level, string category)
        {
            var questions = new List<Question>();
            for (int i = 0; i < 1; i++)
            {
                questions.Add(questionService.GetSpecifiedQuestion("3", level, category));
            }
            for (int i = 0; i < 1; i++)
            {
                questions.Add(questionService.GetSpecifiedQuestion("2", level, category));
            }
            for (int i = 0; i < 1; i++)
            {
                questions.Add(questionService.GetSpecifiedQuestion("1", level, category));
            }

            return questions;
        }
    }
}
