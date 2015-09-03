using Entidades;
using System.Collections.Generic;
using System.Linq;
using Comum.Exceptions;
using Data;

namespace Negocio
{
    public interface IDocenteBusiness : INegocioBase<Teacher>
    {
        int Total(Teacher teacher);
        List<Teacher> SelectWithPagination(Teacher teacher, int paginaAtual);
        bool IsRequiredFieldsFilled(Teacher teacher);
    }

    public class DocenteBusiness : BaseBusiness<Teacher>, IDocenteBusiness
    {
        private readonly IDocenteData _docenteData;
        public DocenteBusiness(IDocenteData data)
            : base(data)
        {
            _docenteData = data;
        }

        public List<Teacher> SelectWithPagination(Teacher teacher, int paginaAtual)
        {
            return _docenteData.SelectWithPagination(teacher, paginaAtual);
        }

        public int Total(Teacher teacher)
        {
            return _docenteData.Total(teacher);
        }

        private static bool IsEscolaridadeFilled(Teacher teacher)
        {
            if (teacher.Education > 0)
                return true;

            throw new RequiredFieldException("Education");
        }

        private static bool IsDisciplinasFilled(Teacher teacher)
        {
            if (teacher.Courses.Any())
                return true;

            throw new RequiredFieldException("Courses");
        }

        public bool IsRequiredFieldsFilled(Teacher teacher)
        {
            return IsEscolaridadeFilled(teacher) && IsDisciplinasFilled(teacher);
        }
    }
}