using System;

namespace Comum.Exceptions
{
    [Serializable]
    public class DuplicatedEntityException : BaseException
    {
        public DuplicatedEntityException() : base(Messages.MI009) { }
    }
}
