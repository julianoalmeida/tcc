namespace Entidades
{
    public class User : BaseEntity
    {
        public virtual string Login { get; set; }

        public virtual string Password { get; set; }

        public virtual int AccessCode { get; set; }
    }
}