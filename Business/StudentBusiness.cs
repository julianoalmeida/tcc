using Entidades;
using System.Collections.Generic;
using System.Linq;
using Comum.Exceptions;
using Data;

namespace Negocio
{
    public interface IStudentBusiness : IBaseBusiness<Student>
    {
        int Total(Student docente);
        string BuildRegistrationNumber(Student student);
        bool IsEducationFieldFilled(Student student);
        List<Student> SelectWithPagination(Student docente, int paginaAtual);
    }

    public class StudentBusinessBusiness : BaseBusinessBusiness<Student>, IStudentBusiness
    {
        private readonly IStudentData _studentData;
        public StudentBusinessBusiness(IStudentData studentData)
            : base(studentData)
        {
            _studentData = studentData;
        }

        public int Total(Student entity)
        {
            return _studentData.Total(entity);
        }

        public List<Student> SelectWithPagination(Student entity, int paginaAtual)
        {
            return _studentData.SelectWithPagination(entity, paginaAtual);
        }

        public string BuildRegistrationNumber(Student entity)
        {
            const int MAX_ID = 1;

            var lastDiscentAdded = _studentData.GetAll().Values.ToList().LastOrDefault();

            return lastDiscentAdded != null
                 ? BuildRegistrationNumberPlusOne(entity, lastDiscentAdded)
                 : FormatRegistrationNumber(entity, MAX_ID);
        }
        
        public bool IsEducationFieldFilled(Student entity)
        {
            if (entity.Education > 0)
                return true;

            throw new RequiredFieldException("Education");
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

        public override void Validate(Student entity)
        {
            throw new System.NotImplementedException();
        }
    }
}