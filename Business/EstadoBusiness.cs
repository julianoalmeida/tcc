using Data;
using Entidades;

namespace Negocio
{
    public interface IEstadoBusiness : INegocioBase<State> { }

    public class EstadoBusiness : BaseBusiness<State>, IEstadoBusiness
    {
        private readonly IStateData _stateData;
        public EstadoBusiness(IStateData data)
            : base(data)
        {
            _stateData = data;
        }
    }
}