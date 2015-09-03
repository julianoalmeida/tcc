using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entidades.Extensions;

namespace Entidades
{
    public class Endereco : Entidade
    {
        public virtual string CodigoUf { get; set; }

        public virtual int IdCidadeBrasil { get; set; }

        public virtual string NomeEndereco { get; set; }

        public virtual string DescricaoBairro { get; set; }

        private string _cep;
        public virtual string Cep
        {
            get { return _cep.RemoveCaracteresMascara(); }
            set { _cep = value; }
        }
    }
}
