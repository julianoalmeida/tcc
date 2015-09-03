using Entidades.Extensions;

namespace Entidades
{
    public class Address : BaseEntity
    {
        public virtual string State { get; set; }

        public virtual int CityId { get; set; }

        public virtual string StreetName { get; set; }

        public virtual string Neighborhood { get; set; }

        private string _zipCode;
        public virtual string ZipCode
        {
            get { return _zipCode.RemoveMaskCharacters(); }
            set { _zipCode = value; }
        }
    }
}
