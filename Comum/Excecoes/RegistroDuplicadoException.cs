using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Comum.Excecoes
{
    [Serializable]
    public class RegistroDuplicadoException : SystemException
    {
        private string Campo { get; set; }

        public RegistroDuplicadoException(string campo, Exception ex)
            : base("Registro Duplicado")
        {
            Campo = "Registro Duplicado";
        }

        public RegistroDuplicadoException()
        {
            Campo = "Registro Duplicado";
        }

        public override string Message
        {
            get
            {
                return String.Format(Mensagens.MI005, Campo);
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
