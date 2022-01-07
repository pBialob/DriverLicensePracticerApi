using AutoMapper;
using DriverLicensePracticerApi.Entities;
using System.Collections.Generic;

namespace DriverLicensePracticerApi.Services.TestGenerator
{
    public abstract class ITestGenerator
    {
        public abstract List<Question> GetTest();
        protected List<Question> GetPrimaryPart(IQuestionService questionService, string level, string category)
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
        protected List<Question> GetSpecialistPart(IQuestionService questionService, string level, string category)
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
