using Data;
using Entidades;

namespace Negocio
{
    public interface IDisciplinaBusiness : INegocioBase<Courses> { }

    public class DisciplinaBusiness : BaseBusiness<Courses>, IDisciplinaBusiness
    {
        private readonly IDisciplinaData _disciplinaData;
        public DisciplinaBusiness(IDisciplinaData data)
            : base(data)
        {
            _disciplinaData = data;
        }
    }
}
