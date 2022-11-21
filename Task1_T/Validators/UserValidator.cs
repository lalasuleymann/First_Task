using FluentValidation;
using Task1_T.Models.Entities;

namespace Task1_T.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(u => u.Password).NotNull();
        }
    }
}
