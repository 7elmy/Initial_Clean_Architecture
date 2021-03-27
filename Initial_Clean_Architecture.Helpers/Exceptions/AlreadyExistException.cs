using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Helpers.Exceptions
{
    public class AlreadyExistException : Exception
    {
        private const string _baseMessage = "Already Exists";
        public AlreadyExistException(string messgae) : base(messgae)
        {

        }
        public class Email : AlreadyExistException
        {
            public Email() :
                base($"This {nameof(Email)} is {_baseMessage}")
            {

            }
        }
    }
}
