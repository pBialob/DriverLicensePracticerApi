using DriverLicensePracticerApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriverLicensePracticerApi.Models
{
    [Keyless]
    [NotMapped]
    public class AnswerDto
    {
        public string QuestionId { get; set; }
        public string Answer { get; set; }
        public string? CorrectAnswer { get; set; } = "Not set";
        public bool Result { get; set; } = false;
        public int TestId { get; set; }
    }
}
