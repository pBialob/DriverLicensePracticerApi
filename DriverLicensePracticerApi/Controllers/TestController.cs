 using DriverLicensePracticerApi.Models;
using DriverLicensePracticerApi.Services.TestGenerator;
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
        private readonly TestFactory _testFactory;
        public TestController(TestFactory testFactory)
        {
            _testFactory = testFactory;
        }
       [HttpGet("{category}")]
        public ActionResult<List<QuestionDto>> GetTest([FromRoute]string category)
        {
            var test = _testFactory.GenerateTest(category).GetTest();

            return test;
        }

     }
 }