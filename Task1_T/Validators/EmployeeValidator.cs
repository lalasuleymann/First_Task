using FluentValidation;
using Task1_T.Models.Entities;

namespace Task1_T.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(e=> e.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(e=> e.Surname).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(e=> e.BirthDate).NotNull().NotEmpty();
        }
    }
}
