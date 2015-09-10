using Entidades;

namespace Negocio.RequiredFieldValidators
{
    public interface IRequiredFieldsValidator
    {
        bool CanValidate(BaseEntity entity);
        void Validate(BaseEntity entity);
    }
}
