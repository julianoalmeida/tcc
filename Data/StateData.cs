using Entidades;
using NHibernate;

namespace Data
{
    public interface IStateData : INHibernateRepository<State> { }

    public class StateData : NHibernateRepository<State>, IStateData
    {
        public StateData(ISession session)
            : base(session)
        { }
    }
}