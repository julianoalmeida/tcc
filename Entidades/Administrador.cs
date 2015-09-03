using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Administrador : Entidade
    {
        public virtual Pessoa Pessoa { get; set; }
    }
}
