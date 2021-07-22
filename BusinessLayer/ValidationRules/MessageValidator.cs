using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Reciever Mail can't be empty!");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Please include minimum 3 characters for Subject Name!");
            RuleFor(x => x.Subject).MaximumLength(50).WithMessage("Please include maximum 50 characters for Subject Name!");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Message Content can't be empty!");
            //RuleFor(x => x.ReceiverMail).EmailAddress();
        }
    }
}
