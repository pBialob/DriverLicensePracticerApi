using DriverLicensePracticerApi.Entities;
using FluentValidation;
using System.Linq;

namespace DriverLicensePracticerApi.Models.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {

        public RegisterUserValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(u => u.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "That email is taken");
                }
            });
        }
    }
}
