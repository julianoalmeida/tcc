using System.ComponentModel;

namespace Entidades.Enums
{
    public enum EducationEnum
    {
        [Description("Ensino Fundamental")]
        EnsinoFundamental = 1,

        [Description("Ensino Médio")]
        EnsinoMedio = 2,

        [Description("Ensino Superior")]
        EnsinoSuperior = 3,

        [Description("Pós Graduação")]
        PosGraduacao = 4
    }
}