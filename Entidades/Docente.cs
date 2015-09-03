using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Docente : Entidade
    {
        public Docente()
        {
            Disciplinas = new List<Disciplina>();
        }

        public virtual Pessoa Pessoa { get; set; }

        public virtual int Escolaridade { get; set; }

        public virtual IList<Disciplina> Disciplinas { get; set; }

    }
}
