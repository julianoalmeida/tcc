using System.Collections.Generic;

namespace Entidades
{
    public sealed class Teacher : BaseEntity
    {
        public Person Person { get; set; }

        public int Education { get; set; }

        public IList<Courses> Courses { get; set; }

        public Teacher()
        {
            Courses = new List<Courses>();
        }
    }
}
