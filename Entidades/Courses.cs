using System.Collections.Generic;

namespace Entidades
{
    public sealed class Courses : BaseEntity
    {
        public Courses()
        {
            Teachers = new List<Teacher>();
        }

        public string Description { get; set; }

        public IList<Teacher> Teachers { get; set; }
    }
}
