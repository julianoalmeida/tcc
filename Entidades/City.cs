namespace Entidades
{
    public class City : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual State State { get; set; }
    }
}
