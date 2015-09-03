using System;
using System.Runtime.Serialization;

namespace Comum.Exceptions
{
    public class BaseException : SystemException
    {
        public override string Message { get;}

        public BaseException(string message) : base(message)
        {
            Message = message;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info?.AddValue("Message", Message);
        }
    }
}
