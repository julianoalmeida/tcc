using System;

namespace Comum.Exceptions
{
    [Serializable]
    public class InvalidEmailException : BaseException
    {
        public InvalidEmailException()
            : base(Messages.INVALID_EMAIL)
        { }
    }
}
