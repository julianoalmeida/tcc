﻿using System;

namespace Comum.Exceptions
{
    [Serializable]
    public class FutureDateException : BaseException
    {
        public FutureDateException() : base(Messages.MI003) { }
    }
}