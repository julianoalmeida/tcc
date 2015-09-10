using Data.BaseRepositories;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IStudentData : IBaseRepositoryRepository<Student> { }

    public class StudentStudentData : BaseRepositoryRepository<Student>, IStudentData
    {
        public StudentStudentData(ISession session) : base(session) { }
    }
}
