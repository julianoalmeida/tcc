using System;
using System.Runtime.Serialization;

namespace Comum.Excecoes
{
    [Serializable]
    public class CpfException : SystemException
    {
        public CpfException(Exception ex)
            : base(ex.Message)
        {

        }

        public CpfException()
        {
        }

        public override string Message
        {
            get
            {
                return Mensagens.MI005;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
        }
    }
}
