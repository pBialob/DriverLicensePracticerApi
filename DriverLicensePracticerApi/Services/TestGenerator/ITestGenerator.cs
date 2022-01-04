using AutoMapper;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using System.Collections.Generic;

namespace DriverLicensePracticerApi.Services.TestGenerator
{
    public abstract class ITestGenerator
    {
        public abstract List<QuestionDto> GetTest();
        protected List<QuestionDto> GetPrimaryPart(IQuestionService questionService, string level, string category)
        {
            var questions = new List<QuestionDto>();
            for (int i = 0; i < 10; i++)
            {
                questions.Add(questionService.GetSpecifiedQuestion("3", level, category));
            }
            for (int i = 0; i < 6; i++)
            {
                questions.Add(questionService.GetSpecifiedQuestion("2", level, category));
            }
            for (int i = 0; i < 4; i++)
            {
                questions.Add(questionService.GetSpecifiedQuestion("1", level, category));
            }

            return questions;
        }
        protected List<QuestionDto> GetSpecialistPart(IQuestionService questionService, string level, string category)
        {
            var questions = new List<QuestionDto>();
            for (int i = 0; i < 6; i++)
            {
                questions.Add(questionService.GetSpecifiedQuestion("3", level, category));
            }
            for (int i = 0; i < 4; i++)
            {
                questions.Add(questionService.GetSpecifiedQuestion("2", level, category));
            }
            for (int i = 0; i < 2; i++)
            {
                questions.Add(questionService.GetSpecifiedQuestion("1", level, category));
            }

            return questions;
        }
    }
}
