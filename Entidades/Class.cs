using System;
using System.Collections.Generic;
using Entidades.Enums;
using Entidades.Extensions;

namespace Entidades
{
    public sealed class Class : BaseEntity
    {
        public Class()
        {
            Students = new List<Student>();
        }

        public int ClassTime { get; set; }

        public int Capacity { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string ClassTimeToString
        {
            get
            {
                ClassesTimeEnum classesTime;
                Enum.TryParse(ClassTime.ToString(), out classesTime);
                return classesTime.GetEnumDescription();
            }
        }

        public string Description { get; set; }

        public Teacher Teacher { get; set; }

        public IList<Student> Students { get; set; }

        public string SelectedStudentsId { get; set; }
    }
}