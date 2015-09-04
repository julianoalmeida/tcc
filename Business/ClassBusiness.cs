using Entidades;
using System.Collections.Generic;
using System.Linq;
using Comum.Exceptions;
using Data;

namespace Negocio
{
    public interface IClassBusiness : INegocioBase<Class>
    {
        int Total(Class model);
        void ValidateTurmaBusinessRules(Class entity);
        List<Class> SelectWithPagination(Class model, int startPage);
    }

    public class ClassBusiness : BaseBusiness<Class>, IClassBusiness
    {
        private readonly IClassData _classData;
        public ClassBusiness(IClassData classData)
            : base(classData)
        {
            _classData = classData;
        }

        public List<Class> SelectWithPagination(Class entity, int startPage)
        {
            return _classData.SelectWithPagination(entity, startPage);
        }

        public int Total(Class model)
        {
            return _classData.Total(model);
        }

        public void ValidateTurmaBusinessRules(Class entity)
        {
            ValidateDiscenteAmoutBiggerThanZero(entity.Students.Count);
            ValidateTurmaIsNoteDuplicated(entity);

            if (string.IsNullOrEmpty(entity.Description))
                throw new RequiredFieldException();

            if (entity.ClassTime == 0)
                throw new RequiredFieldException();

            if (entity.Teacher.Id == 0)
                throw new RequiredFieldException();

            if (!entity.Students.Any())
                throw new RequiredFieldException();
        }

        private void ValidateTurmaIsNoteDuplicated(Class entity)
        {
            var turma = _classData.SelectWithFilter(a => a.Description.ToLower().Equals(entity.Description.ToLower()))
                                  .FirstOrDefault();

            if (turma?.Id != entity.Id)
                throw new DuplicatedEntityException();
        }
        
        private static void ValidateDiscenteAmoutBiggerThanZero(int totalDiscents)
        {
            if (totalDiscents > 20)
                throw new TotalOfSpotsExceededException();
        }
    }
}
