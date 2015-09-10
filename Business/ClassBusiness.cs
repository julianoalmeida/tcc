using Entidades;
using System.Linq;
using Comum;
using Comum.Exceptions;
using Data;
using Negocio.BaseTypes;

namespace Negocio
{
    public interface IClassBusiness : IBaseBusiness<Class> { }

    public class ClassBusiness : BaseBusiness<Class>, IClassBusiness
    {
        private readonly IClassData _repository;

        public ClassBusiness(IClassData repository)
            : base(repository)
        {
            _repository = repository;
        }

        public override void Validate(Class entity)
        {
            ValidateRequiredFields(entity);
            ValidateDiscenteAmoutBiggerThanZero(entity.Students.Count);
            ValidateDuplicatedClass(entity);
        }

        public void ValidateDuplicatedClass(Class entity)
        {
            if (_repository.IsDuplicated(entity))
                throw new DuplicatedEntityException(Messages.DUPLICATED_CLASS);
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
