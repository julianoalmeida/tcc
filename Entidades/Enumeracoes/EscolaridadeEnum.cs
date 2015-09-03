using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Enumeracoes
{
    public enum EscolaridadeEnum
    {
        [Description("Ensino Fundamental")]
        EnsinoFundamental = 1,

        [Description("Ensino Médio")]
        EnsinoMedio = 2,

        [Description("Ensino Superior")]
        EnsinoSuperior = 3,

        [Description("Pós Graduacao")]
        PosGraduacao = 4,
    }
}
