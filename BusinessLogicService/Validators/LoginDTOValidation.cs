using Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validators
{
    public class LoginDTOValidation : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidation()
        {
            RuleFor(l => l.Login)
                .NotNull()
                .MinimumLength(5).WithMessage("{PropertyName} mustn't be null and have at least 5 symbols");

            RuleFor(l => l.Password)
                .NotNull()
                .MinimumLength(6).WithMessage("{PropertyName} mustn't be null and have at least 6 symbols");
        }
    }
}
