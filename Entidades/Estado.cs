using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Entidades
{
    public class Estado : Entidade
    {
        public Estado()
        {
            this.Cidades = new List<Cidade>();
        }
        public virtual string Codigo { get; set; }
        public virtual string Nome { get; set; }

        public virtual Pais Pais { get; set; }

        public virtual IList<Cidade> Cidades { get; set; }

        public virtual List<Cidade> CidadesXML { get { return Cidades.ToList(); } }
    }
}
