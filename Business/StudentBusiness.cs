using System.Linq;
using Entidades;
using Comum.Exceptions;
using Data.BaseRepositories;
using Negocio.BaseTypes;

namespace Negocio
{
    public interface IStudentBusiness : IBaseBusiness<Student> { }

    public class StudentBusiness : BaseBusiness<Student>, IStudentBusiness
    {
        private readonly IPersonBusiness _personBusiness;
        public StudentBusiness(IBaseRepositoryRepository<Student> repotirory, IPersonBusiness personBusiness)
            : base(repotirory)
        {
            _personBusiness = personBusiness;
        }

        public override void Validate(Student entity)
        {
            _personBusiness.Validate(entity.Person);
            ValidateRequiredFields(entity);
        }

        public override Student SaveAndReturn(Student entity)
        {
            entity.RegistrationNumber = BuildRegistrationNumber(entity);
            return base.SaveAndReturn(entity);
        }

        public string BuildRegistrationNumber(Student entity)
        {
            const int MAX_ID = 1;
            var lastDiscentAdded = GetAll().Values.ToList().LastOrDefault();

            return lastDiscentAdded != null
                 ? BuildRegistrationNumberPlusOne(entity, lastDiscentAdded)
                 : FormatRegistrationNumber(entity, MAX_ID);
        }

        private static void ValidateRequiredFields(Student entity)
        {
            if (entity.Education == 0)
                throw new RequiredFieldException();
        }

        private static string FormatRegistrationNumber(Student entity, int maxId)
        {
            return $"{entity.Person.Name}{"UNIP"}{maxId}";
        }

        private static string BuildRegistrationNumberPlusOne(Student entity, Student lastDiscentAdded)
        {
            return lastDiscentAdded.Id == entity.Id
                ? lastDiscentAdded.RegistrationNumber
                : FormatRegistrationNumber(entity, lastDiscentAdded.Id + 1);
        }
    }
}