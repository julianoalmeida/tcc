using System;
using Entidades.Extensions;

namespace Entidades
{
    public class Person : BaseEntity
    {
        public virtual Address Address { get; set; }

        private string _name;
        public virtual string Name
        {
            get { return _name ?? (_name = string.Empty); }
            set { _name = value; }
        }

        private string _zipCode;
        public virtual string ZipCode
        {
            get { return _zipCode == null ? string.Empty : _zipCode.RemoveMaskCharacters(); }
            set { _zipCode = value; }
        }

        public virtual int Sex { get; set; }

        public virtual DateTime? BirthDate { get; set; }

        public virtual string Email { get; set; }

        private string _phoneNumber;
        public virtual string PhoneNumber
        {
            get { return _phoneNumber == null ? string.Empty : _phoneNumber.RemoveMaskCharacters(); }
            set { _phoneNumber = value; }
        }

        private string _mobileNumber;
        public virtual string MobileNumber
        {
            get { return _mobileNumber == null ? string.Empty : _mobileNumber.RemoveMaskCharacters(); }
            set { _mobileNumber = value; }
        }
        
        public virtual int MaritalState { get; set; }
    }
}
