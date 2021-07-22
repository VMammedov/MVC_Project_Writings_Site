using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class TalentValidator : AbstractValidator<Talent>
    {
        public TalentValidator()
        {
            RuleFor(x => x.TalentName).NotEmpty();
            RuleFor(x => x.TalentName).MaximumLength(50).WithMessage("Please include maximum 50 character!");
            RuleFor(x => x.TalentPoint).NotEmpty();
            RuleFor(x => x.TalentPoint).LessThan(101).WithMessage("Please enter numbers between 1 and 100.!");
            RuleFor(x => x.TalentPoint).GreaterThan(0).WithMessage("Please enter numbers between 1 and 100.!");
            RuleFor(x => x.TalentDetail).MaximumLength(250).WithMessage("Please include maximum 250 character!");
        }
    }
}
