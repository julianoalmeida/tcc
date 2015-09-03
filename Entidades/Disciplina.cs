using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{

    public class Disciplina : Entidade
    {
        public Disciplina()
        {
            Docentes = new List<Docente>();
        }
        public virtual string Descricao { get; set; }

        public virtual IList<Docente> Docentes { get; set; }
    }
}
