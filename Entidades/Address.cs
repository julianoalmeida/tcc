namespace Entidades
{
    public class Address : BaseEntity
    {
        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual string Number { get; set; }

        public virtual string ZipCode { get; set; }

        public virtual string StreetName { get; set; }

        public virtual string Neighborhood { get; set; }
    }
}