using System;

namespace Comum.Exceptions
{
    [Serializable]
    public class DuplicatedEntityException : BaseException
    {
        public DuplicatedEntityException() : base(Messages.REGISTER_ALREADY_IN_PLACE) { }
    }
}
