using Data;
using Entidades;

namespace Negocio
{
    public interface ICityBusiness : INegocioBase<City> { }

    public class CityBusiness : BaseBusiness<City>, ICityBusiness
    {
        private readonly ICityData _city;
        public CityBusiness(ICityData city)
            : base(city)
        {
            _city = city;
        }
    }
}