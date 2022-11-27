using Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validators
{
    public class ScoreValidator : AbstractValidator<AppScore>
    {

        public ScoreValidator()
        {
            RuleFor(s => s.Score)
                .NotEmpty().WithMessage("Score can't be empty.")
                .LessThan(11).WithMessage("Score number must be less than 11.");
                
        }


    }
}
