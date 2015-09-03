using System;

namespace Comum.Exceptions
{
    [Serializable]
    public class EmailException : BaseException
    {
        public EmailException()
            : base(Messages.MI006)
        { }
    }
}
