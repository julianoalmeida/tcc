namespace Entidades
{
    public class User : BaseEntity
    {
        public virtual Person Person { get; set; }

        public virtual string Login { get; set; }

        public virtual string Password { get; set; }

        public virtual int AccessCode { get; set; }

        public virtual string Name { get; set; }
    }
}