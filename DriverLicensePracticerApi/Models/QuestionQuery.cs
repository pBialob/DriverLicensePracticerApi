namespace DriverLicensePracticerApi.Models
{
    public class QuestionQuery
    {
        public string? SearchCategory { get; set; }
        public string? SearchPoints { get; set; }
        public string? SearchLevel { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
