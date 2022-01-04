using DriverLicensePracticerApi.Entities;
using System.Collections.Generic;

namespace DriverLicensePracticerApi.Models
{
    public class QuestionDto
    {
        public string QuestionNumber { get; set; }
        public string QuestionContent { get; set; }
        public string? AnswerA { get; set; }
        public string? AnswerB { get; set; }
        public string? AnswerC { get; set; }
        public string MediaPath { get; set; }
        public string QuestionLevel { get; set; }
        public string Points { get; set; }
        public string CategoriesToSet { get; set; }
        public string QuestionOrigin { get; set; }
        public string QuestionReason { get; set; }
        public string SafetyExplanation { get; set; }

        public ICollection<CategoryDto> Categories { get; set; }
    }
}
