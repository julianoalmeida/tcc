using System;
using Comum.Exceptions;
using Data;
using Entidades;
using Entidades.Extensions;

namespace Negocio.RequiredFieldValidators
{
    public class PersonValidator : IRequiredFieldsValidator
    {
        public bool CanValidate(BaseEntity entity)
        {
            return entity is Person;
        }

        public void Validate(BaseEntity entity)
        {
            var person = (Person)entity;
            HasRequiredFieldNotFilled(person);
            ValidateFutureDate(person.BirthDate);
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
    }
}
