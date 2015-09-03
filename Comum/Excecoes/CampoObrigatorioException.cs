using System;
using System.Runtime.Serialization;

namespace Comum.Excecoes
{
    [Serializable]
    public class CampoObrigatorioException : SystemException
    {
        private string Campo { get; set; }

        public CampoObrigatorioException(string campo, Exception ex)
            : base("Existem campos de preenchimento Obrigatório nao preenchidos")
        {
            Campo = "Existem campos de preenchimento Obrigatório nao preenchidos";
        }

        public CampoObrigatorioException()
        {
            Campo = "Existem campos de preenchimento Obrigatório nao preenchidos";
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
