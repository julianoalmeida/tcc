using System;

namespace Comum.Exceptions
{
    [Serializable]
    public class CpfException : BaseException
    {
        public CpfException() : base(string.Format(Messages.REQUIRED_FIELD, "Cpf")) { }
    }
}
