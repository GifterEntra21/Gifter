using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicalLayer.Extensions;
using BusinessLogicalLayer.MessageConst;
using Entities;
using FluentValidation;

namespace BusinessLogicalLayer.Validators
{
    internal class UserValidator : AbstractValidator<User>
    {
        public void ValidateId()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(MessagesValidation.MESSAGE_ERROR_ID_NULL);           
        }
        public void ValidatePassword()
        {
            RuleFor(x => x.Password).MinimumLength(8).WithMessage(MessagesValidation.MESSAGE_ERROR_PASSWORD_LENGHT).MaximumLength(20).WithMessage(MessagesValidation.MESSAGE_ERROR_PASSWORD_LENGHT).IsValidatePassword().WithMessage(MessagesValidation.MESSAGE_ERROR_PASSWORD);
        }

        public void ValidateEmail()
        {
            RuleFor(x => x.Email).MinimumLength(2).WithMessage(MessagesValidation.MESSAGE_ERROR_LAST_NAME_LENGHT).MaximumLength(30).WithMessage(MessagesValidation.MESSAGE_ERROR_LAST_NAME_LENGHT);
        }
        public void ValidateUserName()
        {
            RuleFor(x => x.Email).MinimumLength(5).WithMessage(MessagesValidation.MESSAGE_ERROR_USERNAME_LENGHT).MaximumLength(30).WithMessage(MessagesValidation.MESSAGE_ERROR_USERNAME_LENGHT);
        }

    }
}
