namespace DriverLicensePracticerApi.Entities
{
    public class QuestionCategory
    {
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
