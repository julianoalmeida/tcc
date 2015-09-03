using System.Collections.Generic;

namespace Entidades
{
    public sealed class Country : BaseEntity
    {
        public Country()
        {
            States = new List<State>();
        }

        public string Codigo { get; set; }
        public string Name { get; set; }

        public IList<State> States { get; set; }
    }
}
