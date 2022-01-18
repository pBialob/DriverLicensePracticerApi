using AutoMapper;
using DriverLicensePracticerApi.Entities;
using DriverLicensePracticerApi.Models;
using DriverLicensePracticerApi.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DriverLicensePracticerApi.Services
{
    public interface IQuestionService
    {
        QuestionDto GetRandomQuestionDto();
        PagedResult<QuestionDto> GetAllQuestionsDto(QuestionQuery query);
        public QuestionDto GetSpecifiedQuestionDto(string points, string level, string category);
        public SingleQuestionSolution ResolveSingleQuestion(Answer answer);
    }

    public class QuestionService : IQuestionService
    {
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;

        public QuestionService(IMapper mapper, IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        public QuestionDto GetRandomQuestionDto()
        {   
            var question = _questionRepository.GetRandomQuestion();

            return _mapper.Map<QuestionDto>(question); 
        }

        public PagedResult<QuestionDto> GetAllQuestionsDto(QuestionQuery query)
        {
            var questions = _questionRepository.GetAllQuestions(query);

            return questions;
        }

        public QuestionDto GetSpecifiedQuestionDto(string points, string level, string category)
        {
            var question = _questionRepository.GetSpecifiedQuestion(points, level, category);

            return _mapper.Map<QuestionDto>(question);
        }

        public SingleQuestionSolution ResolveSingleQuestion(Answer answer)
        {
            var question = _questionRepository.GetQuestionByNumber(answer.QuestionNumber);

            answer.ResolveQuestion(question);
            _answerRepository.Add(answer);
            _answerRepository.Save();

            return new SingleQuestionSolution()
            {
                Question = _mapper.Map<QuestionDto>(question),
                Answer = _mapper.Map<AnswerDto>(answer)
            }; 
        }
    }
}
