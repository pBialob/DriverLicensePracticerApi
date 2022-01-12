using DriverLicensePracticerApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Categories { get; set; }
        public string QuestionOrigin { get; set; }
        public string QuestionReason { get; set; }
        public string SafetyExplanation { get; set; }
    }
}
