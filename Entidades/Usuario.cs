using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Usuario : Entidade
    {
        public virtual Pessoa Pessoa { get; set; }

        public virtual string Login { get; set; }

        public virtual string Senha { get; set; }

        public virtual int PerfilAcesso { get; set; }

        public virtual string Nome { get; set; }

    }
}
