using System.Collections.Generic;

namespace Entidades
{
    public  class Courses : BaseEntity
    {
        public Courses()
        {
            Teachers = new List<Teacher>();
        }

        public virtual string Description { get; set; }

        public virtual IList<Teacher> Teachers { get; set; }
    }
}
