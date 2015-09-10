using Entidades;
using System.Linq;
using Comum.Exceptions;
using Data.BaseRepositories;
using Negocio.BaseTypes;

namespace Negocio
{
    public interface IClassBusiness : IBaseBusiness<Class> { }

    public class ClassBusiness : BaseBusiness<Class>, IClassBusiness
    {

        public ClassBusiness(IBaseRepositoryRepository<Class> repository)
            : base(repository)
        { }

        public override void Validate(Class entity)
        {
            ValidateRequiredFields(entity);
            ValidateDiscenteAmoutBiggerThanZero(entity.Students.Count);
            ValidateTurmaIsNoteDuplicated(entity);
        }

        public void ValidateTurmaIsNoteDuplicated(Class entity)
        {
            if (IsDuplicated(FilterHelper.DuplicatedClassCondition(entity)))
                throw new DuplicatedEntityException();
        }

        private static void ValidateRequiredFields(Class entity)
        {
            var hasError = string.IsNullOrEmpty(entity.Description);

            if (entity.ClassTime == 0)
                hasError = true;

            if (entity.Teacher.Id == 0)
                hasError = true;

            if (!entity.Students.Any())
                hasError = true;

            if (hasError) throw new RequiredFieldException();
        }

        private static void ValidateDiscenteAmoutBiggerThanZero(int totalDiscents)
        {
            if (totalDiscents > 20)
                throw new TotalOfSpotsExceededException();
        }
    }
}
