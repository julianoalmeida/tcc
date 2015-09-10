using Comum.Exceptions;
using Entidades;
using Entidades.Extensions;

namespace Negocio.RequiredFieldValidators
{
    public class AddressRequiredFieldValidator : IRequiredFieldsValidator
    {
        public bool CanValidate(BaseEntity entity)
        {
            return entity is Address;
        }

        public void Validate(BaseEntity entity)
        {
            var address = (Address)entity;

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
    }
}
