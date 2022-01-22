using DriverLicensePracticerApi.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriverLicensePracticerApi.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public string GivenAnswer { get; set; }
        public string CorrectAnswer { get; set; } = "Not set";
        public bool Result { get; set; } = false;
        public string QuestionNumber { get; set; }

        public int? TestId { get; set; }
        public virtual Test Test { get; set; }


        public void ResolveQuestion(Question question)
        {
            if(question.CorrectAnswer == GivenAnswer) Result = true;
            CorrectAnswer = question.CorrectAnswer;
        }
    }
}
