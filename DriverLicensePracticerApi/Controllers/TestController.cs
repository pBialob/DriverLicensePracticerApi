using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using DriverLicensePracticerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DriverLicensePracticerApi.Controllers
{
    [ApiController]
    [Route("api/test")]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService = testService;
        }
       [HttpGet("{category}")]
        public ActionResult<TestDto> GetTestQuestions([FromRoute]string category)
        {
            var testQuestions = _testService.CreateTestDto(category);

            return Ok(testQuestions);
        }
        [HttpGet("specified/{testId}")]
        public ActionResult<TestDto> GetSpecifiedTest([FromRoute]int testId)
        {
            var test = _testService.GetSpecifiedTestDto(testId);
            return Ok(test);    
        }
        [HttpPost("{testId}")]
        public ActionResult<Test> SolveTest([FromBody]List<Answer> solution, [FromRoute]int testId)
        {
            var solvedTest = _testService.SolveTest(solution, testId);

            return Ok(solvedTest);
        }

     }
 }