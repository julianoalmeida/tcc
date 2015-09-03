using System;
using System.Runtime.Serialization;

namespace Comum.Excecoes
{
    [Serializable]
    public class EmailException : SystemException
    {
        public EmailException(Exception ex)
            : base(ex.Message)
        {

        }

        public EmailException()
        {
        }

        public override string Message
        {
            get
            {
                return Mensagens.MI006;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (info != null)
            {
                throw new ArgumentNullException("info");
            }
        }
    }
}
