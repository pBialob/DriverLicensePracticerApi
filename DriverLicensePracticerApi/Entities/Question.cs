using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DriverLicensePracticerApi.Entities
{
    public class Question
    {
        public Question()
        {
            this.QuestionCategories = new List<QuestionCategory>();
        }
        public int Id { get; set; }
        public string QuestionName { get; set; }
        public string QuestionNumber { get; set; }
        public string QuestionContent { get; set; }
        public string? AnswerA { get; set; }
        public string? AnswerB { get; set; }
        public string? AnswerC { get; set; }
        public string CorrectAnswer { get; set; }
        public string MediaPath { get; set; }
        public string QuestionLevel { get; set; }
        public string Points { get; set; }
        public string CategoriesToSet { get; set; }
        public string QuestionOrigin { get; set; }
        public string QuestionReason { get; set; }
        public string SafetyExplanation { get; set; }

        public virtual List<QuestionCategory> QuestionCategories { get; set; }
        public virtual List<Test> Tests { get; set; }
    }
}
