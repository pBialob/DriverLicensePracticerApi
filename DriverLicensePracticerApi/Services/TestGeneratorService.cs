using AutoMapper.Configuration;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Repositories;
using Microsoft.Extensions.Options;
using System.Collections.Generic;


namespace DriverLicensePracticerApi.Services.TestGenerator.Tests
{
    public interface ITestGeneratorService
    {
        public List<Question> GetTest(string category);
    }
    public class TestGeneratorService : ITestGeneratorService
    {
        private const string primaryLevel = "PODSTAWOWY";
        private const string specialistLevel = "SPECJALISTYCZNY";
        private readonly IQuestionRepository _questionRepository;
        private readonly IOptions<TestConfiguration> _testConfiguration;

        public TestGeneratorService(IQuestionRepository questionRepository, IOptions<TestConfiguration> testConfiguration)
        {
            _questionRepository = questionRepository;
            _testConfiguration = testConfiguration;
        }

        public List<Question> GetTest(string category)
        {
            var test = new List<Question>();
            test.AddRange(_questionRepository.GetSpecifiedQuestions("3", _testConfiguration.Value.PrimaryPart.Level, category, _testConfiguration.Value.PrimaryPart.ThreePointsQuestionCout)); 
            test.AddRange(_questionRepository.GetSpecifiedQuestions("2", _testConfiguration.Value.PrimaryPart.Level, category, _testConfiguration.Value.PrimaryPart.TwoPointsQuestionCout)); 
            test.AddRange(_questionRepository.GetSpecifiedQuestions("1", _testConfiguration.Value.PrimaryPart.Level, category, _testConfiguration.Value.PrimaryPart.OnePointsQuestionCout)); 
           
            test.AddRange(_questionRepository.GetSpecifiedQuestions("3", _testConfiguration.Value.SpecialistPart.Level, category, _testConfiguration.Value.PrimaryPart.ThreePointsQuestionCout)); 
            test.AddRange(_questionRepository.GetSpecifiedQuestions("2", _testConfiguration.Value.SpecialistPart.Level, category, _testConfiguration.Value.PrimaryPart.TwoPointsQuestionCout)); 
            test.AddRange(_questionRepository.GetSpecifiedQuestions("1", _testConfiguration.Value.SpecialistPart.Level, category, _testConfiguration.Value.PrimaryPart.OnePointsQuestionCout)); 

            return test;
        }
    }
}
