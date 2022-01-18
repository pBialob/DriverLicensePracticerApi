using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using AutoMapper;
using DriverLicensePracticerApi.Repositories;
using System.Linq;

namespace DriverLicensePracticerApi.Services
{
    public interface ITestService
    {
        TestDto CreateTestDto(string category);
        TestDto SolveTest(List<Answer> answers, int testId);
        TestDto GetSpecifiedTestDto(int testId);
    }

    public class TestService : ITestService
    {
        private readonly IHttpContextAccessor _http;
        private readonly IMapper _mapper;
        private readonly IAnswerRepository _answerRepository;
        private readonly ITestRepository _testRepository;
        public TestService(IHttpContextAccessor http, IMapper mapper, IAnswerRepository answerRepository, ITestRepository testRepository)
        {
            _http = http;
            _mapper = mapper;
            _answerRepository = answerRepository;
            _testRepository = testRepository;
        }
        public TestDto CreateTestDto(string category)
        {
            var test = _testRepository.Create(category, Int32.Parse(_http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return _mapper.Map<TestDto>(test);
        }

        public TestDto GetSpecifiedTestDto(int testId)
        {
            var test = _testRepository.GetSpecifiedTest(testId);

            return _mapper.Map<TestDto>(test);
        }

        public TestDto SolveTest(List<Answer> answers, int testId)
        {
            var test = _testRepository.GetSpecifiedTest(testId);    
            test.Answers = answers;
            test.SolveTest();
            _answerRepository.AddMany(answers);
            _answerRepository.Save();

            return _mapper.Map<TestDto>(test);
        }
    }
}
