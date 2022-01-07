using DriverLicensePracticerApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriverLicensePracticerApi.Models
{
    [Keyless]
    [NotMapped]
    public class AnswerDto
    {
        public string QuestionNumber { get; set; }
        public string GivenAnswer { get; set; }
        public string? CorrectAnswer { get; set; } = "Not set";
        public bool Result { get; set; } = false;
    }
}
