using Entidades;
using Entidades.Extensions;
using System;
using System.Linq;
using Comum.Exceptions;
using Data;

namespace Negocio
{
    public interface IPersonBusiness : INegocioBase<Person>
    {
        void ValidadePerson(Person person);
        void ValidateRequiredFields(Person person);
        void ValidateDuplicatedPerson(Person person);
    }

    public class PersonBusiness : BaseBusiness<Person>, IPersonBusiness
    {
        private readonly IPersonData _personData;
        public PersonBusiness(IPersonData data)
            : base(data)
        {
            _personData = data;
        }

        public void ValidateDuplicatedPerson(Person person)
        {
            _personData.IsDuplicated(person);
        }

        public void ValidateRequiredFields(Person person)
        {
            var hasError = false;

            if (string.IsNullOrEmpty(person.Name))
                hasError = true;

            else if (!person.BirthDate.HasValue)
                hasError = true;

            else if (person.Sex == 0)
                hasError = true;
            
            else if (string.IsNullOrEmpty(person.MobileNumber.RemoveMaskCharacters()))
                hasError = true;

            else if (string.IsNullOrEmpty(person.Address.State))
                hasError = true;

            else if (person.Address.CityId == 0)
                hasError = true;

            else if (string.IsNullOrEmpty(person.Address.StreetName))
                hasError = true;

            else if (string.IsNullOrEmpty(person.Address.Neighborhood))
                hasError = true;

            else if (string.IsNullOrEmpty(person.Address.ZipCode))
                hasError = true;

            if (hasError)
                throw new RequiredFieldException();
        }

        public void ValidadePerson(Person person)
        {
            //ValidateDuplicatedPerson(person, databasePerson);
            //ValidateRequiredFields(person);

            //if (ValidaDataAtualFutura(person.BirthDate.Value))
            //    throw new FutureDateException();
            
            //if (!IsValidEmail(person.Email))
            //    throw new EmailException();
        }

        private static void ValidateDuplicatedPerson(Person person, Person databasePerson)
        {
            if (databasePerson?.Id != person.Id)
                throw new DuplicatedEntityException();
        }

        private static bool ValidaDataAtualFutura(DateTime data)
        {
            return data >= DateTime.Today;
        }
    }
}