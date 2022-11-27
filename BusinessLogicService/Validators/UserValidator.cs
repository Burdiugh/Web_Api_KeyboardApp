
using Core.Entities;
using FluentValidation;


namespace Core.Validators
{
    public class UserValidator : AbstractValidator<AppUser>
    {

        public UserValidator()
        {
            RuleFor(u => u.UserName)
                .NotNull()
                .MinimumLength(3).WithMessage("Property {PropertyName} must have more than 2 letters.")
                .MaximumLength(25).WithMessage("Property {PropertyName} must have less than 26 letters.")
                .Matches(@"[A-Z]\w*").WithMessage("Property {PropertyName} must begin with upper case letter.");
        }
    }
}
