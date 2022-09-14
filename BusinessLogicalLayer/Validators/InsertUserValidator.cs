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
            
            base.ValidateEmail();
            base.ValidatePassword();
            base.ValidateUserName();
        }
    }
}
