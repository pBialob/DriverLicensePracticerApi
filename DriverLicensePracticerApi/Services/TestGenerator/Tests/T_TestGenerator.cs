using DriverLicensePracticerApi.Entities;
using System.Collections.Generic;


namespace DriverLicensePracticerApi.Services.TestGenerator.Tests
{
    public class T_TestGenerator : ITestGenerator
    {
        private const string primaryLevel = "PODSTAWOWY";
        private const string specialistLevel = "SPECJALISTYCZNY";
        private const string category = "T";
        private readonly IQuestionService _questionService;
        public T_TestGenerator(IQuestionService questionService)
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
