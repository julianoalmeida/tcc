using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Comum.Excecoes
{
    public class CapacidadeVagasUltrapassadaException : SystemException
    {
        private string Campo { get; set; }

        public CapacidadeVagasUltrapassadaException(string campo, Exception ex)
            : base(ex.Message)
        {
            Campo = campo;
        }

        


        public override string Message
        {
            get
            {
                return String.Format(Mensagens.MI008, Campo);
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
