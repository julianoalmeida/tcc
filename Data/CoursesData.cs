using Entidades;
using NHibernate;

namespace Data
{
    public interface ICoursesData : INHibernateRepository<Courses> { }

    public class CoursesData : NHibernateRepository<Courses>, ICoursesData
    {
        public CoursesData(ISession session)
            : base(session)
        { }
    }
}
