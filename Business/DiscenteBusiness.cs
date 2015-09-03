using Entidades;
using System.Collections.Generic;
using System.Linq;
using Comum.Exceptions;
using Data;

namespace Negocio
{
    public interface IDiscenteBusiness : INegocioBase<Student>
    {
        int Total(Student docente);
        string BuildRegistrationNumber(Student student);
        bool IsEscolaridadeFilled(Student student);
        List<Student> SelectWithPagination(Student docente, int paginaAtual);
    }

    public class DiscenteBusiness : BaseBusiness<Student>, IDiscenteBusiness
    {
        private readonly IDiscenteData _discenteData;
        public DiscenteBusiness(IDiscenteData discenteData)
            : base(discenteData)
        {
            _discenteData = discenteData;
        }

        public int Total(Student docente)
        {
            return _discenteData.Total(docente);
        }

        public List<Student> SelectWithPagination(Student docente, int paginaAtual)
        {
            return _discenteData.SelectWithPagination(docente, paginaAtual);
        }

        public string BuildRegistrationNumber(Student student)
        {
            const int MAX_ID = 1;

            var lastDiscentAdded = _discenteData.GetAll().ToList().LastOrDefault();

            return lastDiscentAdded != null
                 ? BuildRegistrationNumberPlusOne(student, lastDiscentAdded)
                 : FormatRegistrationNumber(student, MAX_ID);
        }
        
        public bool IsEscolaridadeFilled(Student student)
        {
            if (student.Education > 0)
                return true;

            throw new RequiredFieldException("Education");
        }

        private static string FormatRegistrationNumber(Student student, int maxId)
        {
            return $"{student.Person.Name}{"UNIP"}{maxId}";
        }

        private static string BuildRegistrationNumberPlusOne(Student student, Student lastDiscentAdded)
        {   
            return lastDiscentAdded.Id == student.Id
                ? lastDiscentAdded.RegistrationNumber
                : FormatRegistrationNumber(student, lastDiscentAdded.Id + 1);
        }
    }
}