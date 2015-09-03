using System;
using System.Runtime.Serialization;

namespace Comum.Excecoes
{
    public class VincularDisciplinaException : SystemException
    {

        private string Campo { get; set; }

        public VincularDisciplinaException(string campo, Exception ex)
            : base(ex.Message)
        {
            Campo = campo;
        }

        public VincularDisciplinaException(string campo)
        {
            Campo = campo;
        }

        public override string Message
        {
            get
            {
                return String.Format(Mensagens.MI007, Campo);
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            info.AddValue("Campo", Campo);
        }

    }
}
