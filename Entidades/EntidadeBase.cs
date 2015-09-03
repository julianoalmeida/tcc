using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Extensions;

namespace Entidades
{
    public abstract class Entidade
    {
        public virtual int Id { get; set; }

        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        
    }
}
