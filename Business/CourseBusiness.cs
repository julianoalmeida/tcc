using Data;
using Entidades;

namespace Negocio
{
    public interface ICourseBusiness : INegocioBase<Courses> { }

    public class CourseBusiness : BaseBusiness<Courses>, ICourseBusiness
    {
        private readonly ICoursesData _coursesData;
        public CourseBusiness(ICoursesData data)
            : base(data)
        {
            _coursesData = data;
        }
    }
}
