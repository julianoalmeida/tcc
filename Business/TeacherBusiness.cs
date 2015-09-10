using Entidades;
using System.Linq;
using Comum.Exceptions;
using Data;
using Data.BaseRepositories;
using Negocio.BaseTypes;

namespace Negocio
{
    public interface ITeacherBusiness : IBaseBusiness<Teacher> { }

    public class TeacherBusiness : BaseBusiness<Teacher>, ITeacherBusiness
    {
        private readonly IPersonBusiness _personBusiness;
        public TeacherBusiness(ITeacherData data, IPersonBusiness personBusiness)
            : base(data)
        {
            _personBusiness = personBusiness;
        }

        private static void ValidateRequiredFields(Teacher entity)
        {
            if (entity.Education == 0 || entity.Courses.Any())
                throw new RequiredFieldException();
        }

        public override void Validate(Teacher entity)
        {
            _personBusiness.Validate(entity.Person);
            ValidateRequiredFields(entity);
        }
    }
}