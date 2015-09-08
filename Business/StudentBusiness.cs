using Entidades;
using System.Collections.Generic;
using System.Linq;
using Comum.Exceptions;
using Data;

namespace Negocio
{
    public interface IStudentBusiness : INegocioBase<Student>
    {
        int Total(Student docente);
        string BuildRegistrationNumber(Student student);
        bool IsEducationFieldFilled(Student student);
        List<Student> SelectWithPagination(Student docente, int paginaAtual);
    }

    public class StudentBusiness : BaseBusiness<Student>, IStudentBusiness
    {
        private readonly IStudentData _studentData;
        public StudentBusiness(IStudentData studentData)
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

            var lastDiscentAdded = _studentData.GetAll().ToList().LastOrDefault();

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
    }
}