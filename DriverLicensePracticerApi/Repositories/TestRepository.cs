using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Services.TestGenerator.Tests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DriverLicensePracticerApi.Repositories
{
    public interface ITestRepository
    {
        public Test Create(string category, int userId);
        public Test GetSpecifiedTest(int testId);
        void Save();
    }

    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ITestGeneratorService _testGeneratorService;

        public TestRepository(ApplicationDbContext context, ITestGeneratorService testGeneratorService)
        {
            _context = context;
            _testGeneratorService = testGeneratorService;
        }

        public Test Create(string category, int userId)
        {
            var questions = _testGeneratorService.GetTest(category);

            var test = new Test()
            {
                Questions = questions,
                UserId = userId
            };

            _context.Tests.Add(test);
            Save();

            return test;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Test GetSpecifiedTest(int testId)
        {
            var test = _context.Tests
                .Include(x => x.Questions)
                .Include(x => x.Answers)
                .FirstOrDefault(x => x.Id == testId);

            if (test == null) throw new Exception("Test not Found");

            return test;
        }
    }
}
