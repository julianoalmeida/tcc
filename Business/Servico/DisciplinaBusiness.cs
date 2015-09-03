using Comum.Contratos;
using Entidades;
using System.Collections.Generic;

namespace Negocio.Servico
{
    public class DisciplinaBusiness : NegocioBase<Disciplina>, IDisciplinaBusiness
    {
        #region INJEÇÃO

        private readonly IDisciplinaData _disciplinaData;
        public DisciplinaBusiness(IDisciplinaData data)
            : base(data)
        {
            _disciplinaData = data;
        }

        #endregion

    }
}
