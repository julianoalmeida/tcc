using Data;
using Entidades;

namespace Negocio
{
    public interface ICidadeBusiness : INegocioBase<City> { }

    public class CidadeBusiness : BaseBusiness<City>, ICidadeBusiness
    {
        private readonly ICityData _city;
        public CidadeBusiness(ICityData city)
            : base(city)
        {
            _city = city;
        }
    }
}