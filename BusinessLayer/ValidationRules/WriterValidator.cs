using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Writer Name can't be empty!");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Writer SurName can't be empty!");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Please include minimum 2 character!");
            RuleFor(x => x.WriterSurName).MaximumLength(50).WithMessage("Please include maximum 50 character!");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("About can't be empty!");
            RuleFor(x => x.WriterAbout).MaximumLength(99).WithMessage("Please include maximum 100 character!");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Writer Title can't be empty!");
            RuleFor(x => x.WriterTitle).MinimumLength(2).WithMessage("Please include minimum 2 character!");
            RuleFor(x => x.WriterMail).EmailAddress();
        }
    }
}
