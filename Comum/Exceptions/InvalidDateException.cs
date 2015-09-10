using System;

namespace Comum.Exceptions
{
    [Serializable]
    public class InvalidDateException : BaseException
    {
        public InvalidDateException() : base(Messages.INVALID_DATE) { }
    }
}
