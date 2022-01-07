using DriverLicensePracticerApi.Models;
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
        public virtual List<Question> Questions { get; set; }

    }
}
