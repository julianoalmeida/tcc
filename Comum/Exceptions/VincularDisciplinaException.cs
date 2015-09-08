using System;

namespace Comum.Exceptions
{
    [Serializable]
    public class VincularDisciplinaException : BaseException
    {
        public VincularDisciplinaException() : base(Messages.COURSE_REQUIRED) { }
    }
}
