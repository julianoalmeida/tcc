using System.Collections.Generic;

namespace Entidades
{
    public  class Country : BaseEntity
    {
        public Country()
        {
            States = new List<State>();
        }

        public virtual string Codigo { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<State> States { get; set; }
    }
}
