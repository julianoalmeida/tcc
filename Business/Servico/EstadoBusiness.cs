using Comum.Contratos;
using Entidades;
using System.Collections.Generic;

namespace Negocio.Servico
{
    public class EstadoBusiness : NegocioBase<Estado>, IEstadoBusiness
    {
        #region INJEÇÃO

        private readonly IEstadoData _estadoData;
        public EstadoBusiness(IEstadoData data)
            : base(data)
        {
            _estadoData = data;
        }
        #endregion


    }
}
