namespace DriverLicensePracticerApi.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public string GivenAnswer { get; set; }
        public string? CorrectAnswer { get; set; } = "Not set";
        public bool Result { get; set; } = false;
        public int TestId { get; set; }
        public string QuestionNumber { get; set; }
    }
}
