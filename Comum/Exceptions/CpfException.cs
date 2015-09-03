using System;

namespace Comum.Exceptions
{
    [Serializable]
    public class CpfException : BaseException
    {
        public CpfException() : base(string.Format(Messages.MI005, "ZipCode")) { }
    }
}
