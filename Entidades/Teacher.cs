using System.Collections.Generic;

namespace Entidades
{
    public class Teacher : BaseEntity
    {
        public Teacher()
        {
            Courses = new List<Courses>();
        }

        public virtual Person Person { get; set; }

        public virtual int Education { get; set; }

        public virtual IList<Courses> Courses { get; set; }
    }
}
