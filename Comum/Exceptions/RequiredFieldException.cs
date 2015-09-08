using System;

namespace Comum.Exceptions
{
    [Serializable]
    public class RequiredFieldException : BaseException
    {
        public RequiredFieldException(string field) : base(string.Format(Messages.REQUIRED_FIELD, field)) { }

        public RequiredFieldException() : base(Messages.REQUIRED_FIELDS) { }
    }
}