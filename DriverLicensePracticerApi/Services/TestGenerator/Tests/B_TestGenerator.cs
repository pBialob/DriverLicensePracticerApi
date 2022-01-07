using DriverLicensePracticerApi.Entities;
using System.Collections.Generic;

namespace DriverLicensePracticerApi.Services.TestGenerator.Tests
{
    public class B_TestGenerator : ITestGenerator
    {
        private const string primaryLevel = "PODSTAWOWY";
        private const string specialistLevel = "SPECJALISTYCZNY";
        private const string category = "B";
        private readonly IQuestionService _questionService;
        public B_TestGenerator(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public override List<Question> GetTest()
        {
            var test = new List<Question>();
            test.AddRange(base.GetPrimaryPart(_questionService, primaryLevel, category));
            test.AddRange(base.GetSpecialistPart(_questionService, specialistLevel, category));

            return test;
        }
    }
}
