using System.ComponentModel;

namespace Entidades.Enums
{
    public enum AccessProfileEnum
    {
        [Description("Administrador")]
        Administrador = 1,

        [Description("Teacher")]
        Docente = 2,

        [Description("Discente")]
        Discente = 3
    }
}