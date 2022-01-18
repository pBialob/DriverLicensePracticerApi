using DriverLicensePracticerApi.Entities;
using FluentValidation;
using System.Linq;

namespace DriverLicensePracticerApi.Models.Validators
{
    public class QuestionQueryValidator : AbstractValidator<QuestionQuery>
    {
        private int MIN_PAGE_SIZE = 5;
        private int MIN_PAGE_NUMBER = 1;
        private string[] allowerdSortByColumnNames = { nameof(Question.QuestionLevel), nameof(Question.Points), nameof(Question.QuestionNumber) };
        public QuestionQueryValidator()
        {
            RuleFor(q => q.PageNumber).GreaterThanOrEqualTo(MIN_PAGE_NUMBER);
            RuleFor(q => q.PageSize).GreaterThanOrEqualTo(MIN_PAGE_SIZE);
            
            RuleFor(q => q.SortBy).Must(value => string.IsNullOrEmpty(value) || allowerdSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowerdSortByColumnNames)}]");
        }
    }
}
