using System;
using Entidades;

namespace Comum
{
    public class ConditionHelper
    {
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
    }
}
