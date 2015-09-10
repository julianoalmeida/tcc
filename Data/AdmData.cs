using Data.BaseRepositories;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IAdmData : IBaseRepositoryRepository<Adm>
    { }

    public class AdmData : BaseRepositoryRepository<Adm>, IAdmData
    {
        public AdmData(ISession session) : base(session)
        { }
    }
}
