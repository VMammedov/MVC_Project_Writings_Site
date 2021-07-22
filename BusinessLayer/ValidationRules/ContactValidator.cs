using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Name field can't be empty!");
            RuleFor(x => x.UserName).MinimumLength(2).WithMessage("Please include minimum 2 character!");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Subject field can't be empty!");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Please include minimum 3 character!");
            RuleFor(x => x.Subject).MaximumLength(50).WithMessage("Please include maximum 50 character!");
            RuleFor(x => x.Message).NotEmpty().WithMessage("About can't be empty!");
            RuleFor(x => x.UserMail).EmailAddress();
        }
    }
}
