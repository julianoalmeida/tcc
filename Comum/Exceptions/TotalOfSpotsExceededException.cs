using System;

namespace Comum.Exceptions
{
    [Serializable]
    public class TotalOfSpotsExceededException : BaseException
    {
        public TotalOfSpotsExceededException() : base(Messages.MI008)
        { }
    }
}
