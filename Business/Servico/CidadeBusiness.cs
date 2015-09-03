
using Comum.Contratos;
using Entidades;
using System.Linq;
using System.Collections.Generic;

namespace Negocio.Servico
{
    public class CidadeBusiness : NegocioBase<Cidade>, ICidadeBusiness
    {
        private readonly ICidadeData _cidade;
        public CidadeBusiness(ICidadeData cidade)
            : base(cidade)
        {
            _cidade = cidade;
        }

        public List<Cidade> ListarTodos(string siglaEstado)
        {
            return _cidade.Listar().ToList();
        }
    }
}
