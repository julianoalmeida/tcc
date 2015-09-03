using System.ComponentModel;

namespace Entidades.Enums
{
    public enum ClassesTimeEnum
    {
        [Description("Matutino")]
        Manha = 1,

        [Description("Vespertino")]
        Tarde = 2,

        [Description("Noturno")]
        Noite = 3
    }
}