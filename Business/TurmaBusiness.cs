using Entidades;
using System.Collections.Generic;
using System.Linq;
using Comum.Exceptions;
using Data;

namespace Negocio
{
    public interface ITurmaBusiness : INegocioBase<Class>
    {
        int Total(Class model);
        void ValidateTurmaBusinessRules(Class model);
        List<Class> SelectWithPagination(Class model, int paginaAtual);
    }

    public class TurmaBusiness : BaseBusiness<Class>, ITurmaBusiness
    {
        private readonly IClassData _classData;
        public TurmaBusiness(IClassData repositorio)
            : base(repositorio)
        {
            _classData = repositorio;
        }

        public List<Class> SelectWithPagination(Class model, int paginaAtual)
        {
            return _classData.SelectWithPagination(model,  paginaAtual);
        }

        public int Total(Class model)
        {
            return _classData.Total(model);
        }

        public void ValidateTurmaBusinessRules(Class model)
        {
            ValidateDiscenteAmoutBiggerThanZero(model.Students.Count);
            ValidateTurmaIsNoteDuplicated(model);

            if (string.IsNullOrEmpty(model.Description))
                throw new RequiredFieldException();

            if (model.ClassTime == 0)
                throw new RequiredFieldException();

            if (model.Teacher.Id == 0)
                throw new RequiredFieldException();

            if (!model.Students.Any())
                throw new RequiredFieldException();
        }

        private void ValidateTurmaIsNoteDuplicated(Class model)
        {
            var turma = _classData.SelectWithFilter(a => a.Description.ToLower().Equals(model.Description.ToLower()))
                                  .FirstOrDefault();

            if (turma?.Id != model.Id)
                throw new DuplicatedEntityException();
        }
        
        private static void ValidateDiscenteAmoutBiggerThanZero(int totalDiscents)
        {
            if (totalDiscents > 20)
                throw new TotalOfSpotsExceededException();
        }
    }
}
