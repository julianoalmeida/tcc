using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Enumeracoes
{
    public enum TurnoEnum
    {
        [Description("Matutino")]
        Manha = 1,

        [Description("Vespertino")]
        Tarde = 2,

        [Description("Noturno")]
        Noite = 3
    }
}
