using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Validators
{
    internal class InsertUserValidator : UserValidator
    {
        public InsertUserValidator()
        {
            base.ValidateFirstName();
            base.ValidateLastName();
            base.ValidatePassword();
            base.ValidateUserName();
        }
    }
}
