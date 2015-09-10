using System;
using Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Comum.Exceptions;
using Data;

namespace Negocio
{
    public interface IClassBusiness : IBaseBusiness<Class>
    {
        int Total(Class model);
        List<Class> SelectWithPagination(Class model, int startPage);
    }

    public class ClassBusinessBusiness : BaseBusinessBusiness<Class>, IClassBusiness
    {
        private readonly IClassData _classData;
        public ClassBusinessBusiness(IClassData classData)
            : base(classData)
        {
            _classData = classData;
        }

        public override void Validate(Class entity)
        {
            ValidateRequiredFields(entity);
            ValidateDiscenteAmoutBiggerThanZero(entity.Students.Count);
            ValidateTurmaIsNoteDuplicated(entity);
        }

        public List<Class> SelectWithPagination(Class entity, int startPage)
        {
            return _classData.SelectWithPagination(entity, startPage);
        }

        public int Total(Class model)
        {
            return _classData.Total(model);
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

        private void ValidateTurmaIsNoteDuplicated(Class entity)
        {
            var turma = _classData.SelectWithFilter(ClassFilterCondition(entity))
                .Values.FirstOrDefault();

            if (turma?.Id != entity.Id)
                throw new DuplicatedEntityException();
        }

        private static Expression<Func<Class, bool>> ClassFilterCondition(Class entity)
        {
            return a => a.Description.ToLower().Equals(entity.Description.ToLower());
        }

        private static void ValidateDiscenteAmoutBiggerThanZero(int totalDiscents)
        {
            if (totalDiscents > 20)
                throw new TotalOfSpotsExceededException();
        }


    }
}
