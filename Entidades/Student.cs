using System.Collections.Generic;

namespace Entidades
{
    public class Student : BaseEntity
    {
        public virtual Person Person { get; set; }

        public virtual string RegistrationNumber { get; set; }

        public virtual int Education { get; set; }

        public virtual IList<Class> Classes { get; set; }
    }
}
