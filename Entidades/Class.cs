using System;
using System.Collections.Generic;
using Entidades.Enums;
using Entidades.Extensions;

namespace Entidades
{
    public class Class : BaseEntity
    {
        public Class()
        {
            Students = new List<Student>();
        }

        public virtual int ClassTime { get; set; }

        public virtual int Capacity { get; set; }

        public virtual DateTime? BeginDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual string ClassTimeToString
        {
            get
            {
                ClassesTimeEnum classesTime;
                Enum.TryParse(ClassTime.ToString(), out classesTime);
                return classesTime.GetEnumDescription();
            }
        }

        public virtual string Description { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual IList<Student> Students { get; set; }

        public virtual  string SelectedStudentsId { get; set; }
    }
}