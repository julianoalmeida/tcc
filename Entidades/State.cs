using System.Collections.Generic;
using System.Linq;

namespace Entidades
{
    public sealed class State : BaseEntity
    {
        public State()
        {
            Cities = new List<City>();
        }
        public string Code { get; set; }
        public string Name { get; set; }

        public Country Country { get; set; }

        public IList<City> Cities { get; set; }

        public List<City> XmlCities => Cities.ToList();
    }
}
