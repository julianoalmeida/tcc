using System;
using System.Linq.Expressions;
using Entidades;

namespace Negocio
{
    public class FilterHelper
    {
        public static Func<Person, bool> PersonPaginationCondition(Person person)
        {
            return a => a.Name == person.Name && a.BirthDate?.Date == person.BirthDate?.Date && a.Id != person.Id;
        }

        public static Func<Class, bool> ClassFilterCondition(Class entity)
        {
            return
                a =>
                    string.IsNullOrEmpty(entity.Description) ||
                    a.Description.ToLower().Contains(entity.Description.ToLower())
                    && entity.ClassTime == 0 || a.ClassTime == entity.ClassTime;
        }

        public static Func<Adm, bool> AdmFilterCondition(Adm adm)
        {
            return a => string.IsNullOrEmpty(adm.Person.Name) ||
                        a.Person.Name.ToLower().Contains(adm.Person.Name.ToLower());
        }

        public static Func<Student, bool> StudentFilterCondition(Student student)
        {
            return a =>
                string.IsNullOrEmpty(student.Person.Name) ||
                a.Person.Name.ToLower().Contains(student.Person.Name.ToLower());
        }

        public static Func<Teacher, bool> TeacherFilterCondition(Teacher teacher)
        {
            return a =>
                string.IsNullOrEmpty(teacher.Person.Name) ||
                a.Person.Name.ToLower().Contains(teacher.Person.Name.ToLower());
        }

        public static Func<Person, bool> DuplicatedPersonCondition(Person person)
        {
            return a => a.Name == person.Name && a.BirthDate?.Date == person.BirthDate?.Date && a.Id != person.Id;
        }

        public static Func<Adm, bool> DuplicatedAdmCondition(Adm entity)
        {
            return
               a =>
                   a.Person.Name == entity.Person.Name
                   && a.Person.BirthDate?.Date == entity.Person.BirthDate?.Date
                   && a.Id != entity.Person.Id;
        }

        public static Func<Teacher, bool> DuplicatedTeacherCondition(Teacher entity)
        {
            return
               a =>
                   a.Person.Name == entity.Person.Name
                   && a.Person.BirthDate?.Date == entity.Person.BirthDate?.Date
                   && a.Id != entity.Person.Id;
        }

        public static Func<Student, bool> DuplicatedStudentCondition(Student entity)
        {
            return
               a =>
                   a.Person.Name == entity.Person.Name
                   && a.Person.BirthDate?.Date == entity.Person.BirthDate?.Date
                   && a.Id != entity.Person.Id;
        }

        public static Expression<Func<Class, bool>> DuplicatedClassCondition(Class entity)
        {
            return a => a.Description.ToLower().Equals(entity.Description.ToLower()) && a.Id != entity.Id;
        }
    }
}
