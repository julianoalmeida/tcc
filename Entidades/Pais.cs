using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pais : Entidade
    {
        public Pais()
        {
            this.Estados = new List<Estado>();
        }

        public virtual string Codigo { get; set; }
        public virtual string Nome { get; set; }

        public virtual IList<Estado> Estados { get; set; }
    }
}
