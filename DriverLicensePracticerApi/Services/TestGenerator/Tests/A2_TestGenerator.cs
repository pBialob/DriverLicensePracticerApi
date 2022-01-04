using DriverLicensePracticerApi.Models;
using System.Collections.Generic;


namespace DriverLicensePracticerApi.Services.TestGenerator.Tests
{
    public class A2_TestGenerator : ITestGenerator
    {
        private const string primaryLevel = "PODSTAWOWY";
        private const string specialistLevel = "SPECJALISTYCZNY";
        private const string category = "A2";
        private readonly IQuestionService _questionService;
        public A2_TestGenerator(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public override List<QuestionDto> GetTest()
        {
            var test = new List<QuestionDto>();
            test.AddRange(base.GetPrimaryPart(_questionService, primaryLevel, category));
            test.AddRange(base.GetSpecialistPart(_questionService, specialistLevel, category));

            return test;
        }
    }
}
