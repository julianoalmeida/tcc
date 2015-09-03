using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Discente : Entidade
    {
        public virtual Pessoa Pessoa { get; set; }

        public virtual string Matricula { get; set; }

        public virtual int Escolaridade { get; set; }

        public virtual IList<Turma> Turmas { get; set; }
    }
}
