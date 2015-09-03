using System;

namespace Comum.Exceptions
{
    [Serializable]
    public class RequiredFieldException : BaseException
    {
        public RequiredFieldException(string field) : base(string.Format(Messages.MI005, field)) { }

        public RequiredFieldException() : base(Messages.MI010) { }
    }
}