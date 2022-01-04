﻿using DriverLicensePracticerApi.Models;
using System.Collections.Generic;


namespace DriverLicensePracticerApi.Services.TestGenerator.Tests
{
    public class AM_TestGenerator : ITestGenerator
    {
        private const string primaryLevel = "PODSTAWOWY";
        private const string specialistLevel = "SPECJALISTYCZNY";
        private const string category = "AM";
        private readonly IQuestionService _questionService;
        public AM_TestGenerator(IQuestionService questionService)
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