using System;
using System.Text.RegularExpressions;
using Entidades;
using Comum.Exceptions;
using Data;
using Entidades.Extensions;

namespace Negocio
{
    public interface IPersonBusiness : IBaseBusiness<Person> { }

    public class PersonBusinessBusiness : BaseBusinessBusiness<Person>, IPersonBusiness
    {
        private readonly IPersonData _personData;
        public PersonBusinessBusiness(IPersonData data)
            : base(data)
        {
            _personData = data;
        }

        public override void Validate(Person person)
        {
            HasRequiredFieldNotFilled(person);
            ValidateFutureDate(person.BirthDate);
            ValidateEmail(person.Email);
            ValidateAddress(person.Address);
            ValidateDuplicatedPerson(person);
        }

        private static void HasRequiredFieldNotFilled(Person person)
        {
            var hasError = string.IsNullOrEmpty(person.Name);

            if (string.IsNullOrEmpty(person.Email))
                hasError = true;

            else if (!person.BirthDate.HasValue)
                hasError = true;

            else if (person.Sex == 0)
                hasError = true;

            else if (string.IsNullOrEmpty(person.MobileNumber.RemoveMaskCharacters().RemoveEmptySpaces()))
                hasError = true;

            if (hasError)
                throw new RequiredFieldException();
        }

        private static void ValidateFutureDate(DateTime? date)
        {
            if (date == null || date.Value.Date > DateTime.Today.Date)
                throw new InvalidDateException();
        }

        private static void ValidateEmail(string email)
        {
            var validEmail =
                new Regex(
                    @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (string.IsNullOrEmpty(email) || validEmail.IsMatch(email))
                throw new InvalidEmailException();
        }

        private static void ValidateAddress(Address address)
        {
            if (address == null) return;

            var hasError = string.IsNullOrEmpty(address.State);

            if (address.CityId == 0)
                hasError = true;

            else if (string.IsNullOrEmpty(address.StreetName))
                hasError = true;

            else if (string.IsNullOrEmpty(address.Neighborhood))
                hasError = true;

            else if (string.IsNullOrEmpty(address.ZipCode.RemoveMaskCharacters().RemoveEmptySpaces()))
                hasError = true;

            if (hasError)
                throw new RequiredFieldException();

        }

        private void ValidateDuplicatedPerson(Person person)
        {
            if (_personData.IsDuplicated(person))
                throw new DuplicatedEntityException();
        }
    }
}