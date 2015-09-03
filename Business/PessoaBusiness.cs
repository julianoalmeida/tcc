using Entidades;
using Entidades.Extensions;
using System;
using System.Linq;
using Comum.Exceptions;
using Data;

namespace Negocio
{
    public interface IPessoaBusiness : INegocioBase<Person>
    {
        void ValidadePerson(Person person);
        void ValidateRequiredFields(Person person);
        void ValidateDuplicatedPerson(Person person);
    }

    public class PessoaBusiness : BaseBusiness<Person>, IPessoaBusiness
    {
        private readonly IPessoaData _pessoaData;
        public PessoaBusiness(IPessoaData data)
            : base(data)
        {
            _pessoaData = data;
        }

        public void ValidateDuplicatedPerson(Person person)
        {
            _pessoaData.IsDuplicated(person);
        }
        
        public void ValidateRequiredFields(Person person)
        {
            var camposPreenchidos = false;

            if (string.IsNullOrEmpty(person.Name))
                camposPreenchidos = true;

            else if (!person.BirthDate.HasValue)
                camposPreenchidos = true;

            else if (person.Sex == 0)
                camposPreenchidos = true;

            else if (person.MaritalState == 0)
                camposPreenchidos = true;

            else if (string.IsNullOrEmpty(person.MobileNumber.RemoveMaskCharacters()))
                camposPreenchidos = true;

            else if (string.IsNullOrEmpty(person.Address.State))
                camposPreenchidos = true;

            else if (person.Address.CityId == 0)
                camposPreenchidos = true;

            else if (string.IsNullOrEmpty(person.Address.StreetName))
                camposPreenchidos = true;

            else if (string.IsNullOrEmpty(person.Address.Neighborhood))
                camposPreenchidos = true;

            else if (string.IsNullOrEmpty(person.Address.ZipCode))
                camposPreenchidos = true;

            if (camposPreenchidos)
                throw new RequiredFieldException();
        }

        public void ValidadePerson(Person person)
        {
            var pessoaBd = _pessoaData.SelectWithFilter(a => a.ZipCode.Equals(person.ZipCode)).FirstOrDefault();

            if (pessoaBd?.Id != person.Id)
                throw new DuplicatedEntityException();

            ValidateRequiredFields(person);

            if (ValidaDataAtualFutura(person.BirthDate.Value))
                throw new FutureDateException();

            if (!IsValidCpf(person.ZipCode))
                throw new CpfException();

            if (!IsValidEmail(person.Email))
                throw new EmailException();
        }

        private static bool ValidaDataAtualFutura(DateTime data)
        {
            return data >= DateTime.Today;
        }
    }
}