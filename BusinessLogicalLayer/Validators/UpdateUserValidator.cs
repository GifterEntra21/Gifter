using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Validators
{
    internal class UpdateUserValidator: UserValidator
    {
        public UpdateUserValidator()
        {
            base.ValidateUserName();
            base.ValidateEmail();
        }
    }
}
