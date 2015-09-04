using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Table("Pessoa");
            Id(a => a.Id, "Id");
            References(a => a.Address, "IdEndereco").Not.Nullable().Cascade.All();
            Map(a => a.Name, "Nome").Not.Nullable();
            Map(a => a.Cpf, "Cpf").Not.Nullable();
            Map(a => a.Sex, "Sexo").Not.Nullable();
            Map(a => a.BirthDate, "DataNascimento").Not.Nullable();
            Map(a => a.Email, "Email").Nullable();
            Map(a => a.PhoneNumber, "Telefone").Nullable();
            Map(a => a.MobileNumber, "Celular").Nullable();
            Map(a => a.MaritalState, "EstadoCivil").Not.Nullable();
        }
    }
}