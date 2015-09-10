using System;

namespace Comum.Exceptions
{
    [Serializable]
    public class DuplicatedEntityException : BaseException
    {
        public DuplicatedEntityException(string errorMessage) : base(errorMessage) { }
    }
}
