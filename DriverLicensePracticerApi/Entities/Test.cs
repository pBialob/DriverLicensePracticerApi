using DriverLicensePracticerApi.Models;
using DriverLicensePracticerApi.Repositories;
using DriverLicensePracticerApi.Services.TestGenerator.Tests;
using System;
using System.Collections.Generic;

namespace DriverLicensePracticerApi.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public List<Answer>? Answers { get; set; }
        public int Score { get; set; } = 0;
        public bool IsResolved { get; set; } = false;

        public int UserId { get; set; }
        public virtual User  User { get; set; }
        public virtual List<Question> Questions { get; set; }

        public void SolveTest(List<Answer> answers)
        {
            if (IsResolved) throw new Exception("Test has been already resolved");
            Answers = answers;
            foreach (var answer in Answers)
            {
                var question = Questions.Find(Question => Question.QuestionNumber == answer.QuestionNumber);
                if (question == null) throw new Exception("Question not found");
                if (answer.GivenAnswer == question.CorrectAnswer)
                {
                    Score += Int32.Parse(question.Points);
                    answer.Result = true;
                }
                answer.CorrectAnswer = question.CorrectAnswer;
            }

            IsResolved = true;
        }
    }
}
