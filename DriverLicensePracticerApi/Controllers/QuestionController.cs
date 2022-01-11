using AutoMapper;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using DriverLicensePracticerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DriverLicensePracticerApi.Controllers
{
    [Route("api/question")]
    [ApiController]
   // [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;
        public QuestionController(IQuestionService service, IMapper mapper)
        {
            _questionService = service;
            _mapper = mapper;
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
        [HttpGet("specified")]
        public ActionResult<QuestionDto> GetSpecifiedQuestion([FromBody]RandomSpecifiedDto dto)
        {
            var question = _questionService.GetSpecifiedQuestion(dto.Points, dto.Level, dto.Category);
            var questionDto = _mapper.Map<QuestionDto>(question);

            return Ok(questionDto);
        }
        [HttpPost("resolve")]
        public ActionResult<SingleQuestionSolution> ResolveRandomQuestion([FromBody]Answer answer)
        {
            var solution = _questionService.ResolveSingleQuestion(answer);

            return Ok(solution);
        }
    }
}
