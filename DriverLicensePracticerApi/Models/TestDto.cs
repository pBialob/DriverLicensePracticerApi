using DriverLicensePracticerApi.Entities;
using System.Collections.Generic;

namespace DriverLicensePracticerApi.Models
{
    public class TestDto
    {
        public int TestId { get; set; }
        public List<QuestionDto> Questions { get; set; }
        public List<Answer>? Answers { get; set; }
        public int? Score { get; set; }
        public bool IsResolved  { get; set; } = false;

    }
}
