using Data;
using Entidades;

namespace Negocio
{
    public interface ICidadeBusiness : INegocioBase<City> { }

    public class CidadeBusiness : BaseBusiness<City>, ICidadeBusiness
    {
        private readonly ICidadeData _cidade;
        public CidadeBusiness(ICidadeData cidade)
            : base(cidade)
        {
            _cidade = cidade;
        }
    }
}