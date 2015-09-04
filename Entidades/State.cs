using System.Collections.Generic;
using System.Linq;

namespace Entidades
{
    public class State : BaseEntity
    {
        public State()
        {
            Cities = new List<City>();
        }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

        public virtual Country Country { get; set; }

        public virtual IList<City> Cities { get; set; }

        public virtual List<City> XmlCities => Cities.ToList();
    }
}
