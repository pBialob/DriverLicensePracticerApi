using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Repositories;
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
        public TestGeneratorService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;   
        }
        public List<Question> GetTest(string category)
        {
            var test = new List<Question>();
            test.AddRange(GetPrimaryPart(primaryLevel, category));
            test.AddRange(GetSpecialistPart(specialistLevel, category));

            return test;
        }
        private List<Question> GetPrimaryPart(string level, string category)
        {
            var questions = new List<Question>();
            while(questions.Count != 10)
            {
                var question = _questionRepository.GetSpecifiedQuestion("3", level, category);
                if (!HasQuestionRepeated(questions, question))
                {
                    questions.Add(question);
                }
            }
            while (questions.Count != 16)
            {
                var question = _questionRepository.GetSpecifiedQuestion("2", level, category);
                if (!HasQuestionRepeated(questions, question))
                {
                    questions.Add(question);
                }
            }
            while (questions.Count != 19)
            {
                var question = _questionRepository.GetSpecifiedQuestion("1", level, category);
                if (!HasQuestionRepeated(questions, question))
                {
                    questions.Add(question);
                }
            }

            return questions;
        }
        private List<Question> GetSpecialistPart(string level, string category)
        {
            var questions = new List<Question>();
            while(questions.Count != 10)
            {
                var question = _questionRepository.GetSpecifiedQuestion("3", level, category);
                if (!HasQuestionRepeated(questions, question))
                {
                    questions.Add(question);
                }
            }
            while (questions.Count != 16)
            {
                var question = _questionRepository.GetSpecifiedQuestion("2", level, category);
                if (!HasQuestionRepeated(questions, question))
                {
                    questions.Add(question);
                }
            }
            while (questions.Count != 19)
            {
                var question = _questionRepository.GetSpecifiedQuestion("1", level, category);
                if (!HasQuestionRepeated(questions, question))
                {
                    questions.Add(question);
                }
            }

            return questions;
        }

        bool HasQuestionRepeated(List<Question> questions, Question question)
        {
            if (questions.Contains(question))
                return true;
            return false;
        }
    }
}
