using AutoMapper;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using DriverLicensePracticerApi.Services.TestGenerator.Tests;
using System;
using System.Collections.Generic;

namespace DriverLicensePracticerApi.Services.TestGenerator
{
    public  class TestFactory
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IQuestionService _questionService;
        public TestFactory(ApplicationDbContext context, IMapper mapper, IQuestionService questionService)
        {
            _context = context;
            _mapper = mapper;
            _questionService = questionService;
        }
        public  ITestGenerator GenerateTest(string category)
        {
            switch (category)
            {
                case "A":
                    return new A_TestGenerator(_questionService);
                case "B":
                    return new B_TestGenerator(_questionService);
                case "C":
                    return new C_TestGenerator(_questionService);
                case "D":
                    return new D_TestGenerator(_questionService);
                case "T":
                    return new T_TestGenerator(_questionService);
                case "AM":
                    return new AM_TestGenerator(_questionService);
                case "A1":
                    return new A1_TestGenerator(_questionService);
                case "A2":
                    return new A2_TestGenerator(_questionService);
                case "B1":
                    return new B1_TestGenerator(_questionService);
                case "C1":
                    return new C1_TestGenerator(_questionService);
                case "D1":
                    return new D1_TestGenerator(_questionService);
                case "PT":
                    return new PT_TestGenerator(_questionService);
                default:
                    throw new Exception($"Test {category} can't be handled");
            }
        }

    }
}
