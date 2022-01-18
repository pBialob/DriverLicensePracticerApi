using DriverLicensePracticerApi.Entities;
using System.Collections.Generic;

namespace DriverLicensePracticerApi.Models
{
    public class TestDto
    {
        public int TestId { get; set; }
        public virtual List<QuestionDto> Questions { get; set; }
        public virtual List<AnswerDto>? Answers { get; set; }
        public int? Score { get; set; }
        public bool IsResolved  { get; set; } = false;

    }
}
