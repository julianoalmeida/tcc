using Data;
using Entidades;

namespace Negocio
{
    public interface IDisciplinaBusiness : INegocioBase<Courses> { }

    public class DisciplinaBusiness : BaseBusiness<Courses>, IDisciplinaBusiness
    {
        private readonly ICoursesData _coursesData;
        public DisciplinaBusiness(ICoursesData data)
            : base(data)
        {
            _coursesData = data;
        }
    }
}
