using Data;
using Entidades;

namespace Negocio
{
    public interface IStateBusiness : INegocioBase<State> { }

    public class StateBusiness : BaseBusiness<State>, IStateBusiness
    {
        private readonly IStateData _stateData;
        public StateBusiness(IStateData data)
            : base(data)
        {
            _stateData = data;
        }
    }
}