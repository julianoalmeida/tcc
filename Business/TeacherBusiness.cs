using Entidades;
using System.Collections.Generic;
using System.Linq;
using Comum.Exceptions;
using Data;

namespace Negocio
{
    public interface ITeacherBusiness : IBaseBusiness<Teacher>
    {
        int Total(Teacher teacher);
        List<Teacher> SelectWithPagination(Teacher teacher, int paginaAtual);
        bool IsRequiredFieldsFilled(Teacher teacher);
    }

    public class TeacherBusinessBusiness : BaseBusinessBusiness<Teacher>, ITeacherBusiness
    {
        private readonly ITeacherData _teacherData;
        public TeacherBusinessBusiness(ITeacherData data)
            : base(data)
        {
            _teacherData = data;
        }

        public List<Teacher> SelectWithPagination(Teacher entity, int paginaAtual)
        {
            return _teacherData.SelectWithPagination(entity, paginaAtual);
        }

        public int Total(Teacher teacher)
        {
            return _teacherData.Total(teacher);
        }

        private static bool IsEscolaridadeFilled(Teacher entity)
        {
            if (entity.Education > 0)
                return true;

            throw new RequiredFieldException("Education");
        }

        private static bool IsDisciplinasFilled(Teacher entity)
        {
            if (entity.Courses.Any())
                return true;

            throw new RequiredFieldException("Courses");
        }

        public bool IsRequiredFieldsFilled(Teacher entity)
        {
            return IsEscolaridadeFilled(entity) && IsDisciplinasFilled(entity);
        }

        public override void Validate(Teacher entity)
        {
            throw new System.NotImplementedException();
        }
    }
}