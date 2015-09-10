using Data.BaseRepositories;
using Entidades;
using NHibernate;

namespace Data
{
    public interface ITeacherData : IBaseRepositoryRepository<Teacher> { }

    public class TeacherData : BaseRepositoryRepository<Teacher>, ITeacherData
    {
        public TeacherData(ISession session) : base(session) { }
    }
}
