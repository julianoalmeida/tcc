using Entidades;
using NHibernate;

namespace Data
{
    public interface IPessoaData : IRepositorio<Person>
    {
        bool IsDuplicated(Person person);
    }

    public class PessoaData : RepositorioNHibernate<Person>, IPessoaData
    {
        public PessoaData(ISession session)
            : base(session)
        { }

        public bool IsDuplicated(Person person)
        {
            throw new System.NotImplementedException();
        }
    }
}
