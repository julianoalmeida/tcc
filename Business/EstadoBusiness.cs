using Data;
using Entidades;

namespace Negocio
{
    public interface IEstadoBusiness : INegocioBase<State> { }

    public class EstadoBusiness : BaseBusiness<State>, IEstadoBusiness
    {
        private readonly IEstadoData _estadoData;
        public EstadoBusiness(IEstadoData data)
            : base(data)
        {
            _estadoData = data;
        }
    }
}