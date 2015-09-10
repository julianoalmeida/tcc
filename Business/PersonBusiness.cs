using Entidades;
using Comum.Exceptions;
using Data;

namespace Negocio
{
    public interface IPersonBusiness : INegocioBase<Person> { }

    public class PersonBusiness : BaseBusiness<Person>, IPersonBusiness
    {
        private readonly IPersonData _personData;
        public PersonBusiness(IPersonData data)
            : base(data)
        {
            _personData = data;
        }

        private void ValidateDuplicatedPerson(Person person)
        {
            if (_personData.IsDuplicated(person))
                throw new DuplicatedEntityException();
        }

        public override Person SaveAndReturn(Person entidade)
        {
            ValidateDuplicatedPerson(entidade);
            return base.SaveAndReturn(entidade);
        }
    }
}