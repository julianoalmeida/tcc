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

        public virtual int Sex { get; set; }

        public virtual string Email { get; set; }

        private string _mobileNumber;
        public virtual string MobileNumber
        {
            get { return _mobileNumber == null ? string.Empty : _mobileNumber.RemoveMaskCharacters(); }
            set { _mobileNumber = value; }
        }

        public virtual User User { get; set; }
    }
}
