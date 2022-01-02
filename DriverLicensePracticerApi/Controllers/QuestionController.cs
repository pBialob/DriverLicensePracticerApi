using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using DriverLicensePracticerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DriverLicensePracticerApi.Controllers
{
    [Route("api/question")]
    //[ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService service)
        {
            _questionService = service;
        }
        [HttpGet]
        public ActionResult<IEnumerable<QuestionDto>> GetAllQuestions()
        {
            var questions = _questionService.GetAllQuestions();
            return Ok(questions);
        }
        [HttpGet("random")]
        public ActionResult<QuestionDto> GetRandomQuestion()
        {
            var question = _questionService.GetRandomQuestion();
            return Ok(question);
        }
    }
}
