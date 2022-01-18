using DriverLicensePracticerApi.Models;
using DriverLicensePracticerApi.Repositories;
using DriverLicensePracticerApi.Services.TestGenerator.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DriverLicensePracticerApi.Entities
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public virtual List<Answer>? Answers { get; set; }
        public int Score { get; set; } = 0;
        public bool IsResolved { get; set; } = false;

        public int UserId { get; set; }
        public virtual User  User { get; set; }
        public virtual List<Question> Questions { get; set; }

          public void SolveTest()
        {
            if (this.IsResolved) throw new Exception("Test has been already resolved");
            foreach (var answer in Answers)
            {
                var question = Questions.Find(question => question.QuestionNumber == answer.QuestionNumber);
                if (question == null) throw new Exception("Question not found");
                if (answer.GivenAnswer == question.CorrectAnswer)
                {
                    Score += Int32.Parse(question.Points);
                    answer.Result = true;
                }
                answer.CorrectAnswer = question.CorrectAnswer;
                answer.TestId = Id;
            }
            this.IsResolved = true;
        }
    }
}
