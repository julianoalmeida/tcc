using System;
using System.Runtime.Serialization;

namespace Comum.Excecoes
{
    [Serializable]
    public class DataAtualFuturaException : SystemException
    {
        public DataAtualFuturaException(Exception ex)
            : base(ex.Message)
        {

        }

        public DataAtualFuturaException()
        {

        }

        public override string Message
        {
            get
            {
                return Mensagens.MI003;
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
