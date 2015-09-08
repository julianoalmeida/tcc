﻿using System.ComponentModel;

namespace Entidades.Enums
{   
    public enum AccessProfileEnum
    {
        [Description("Administrador")]
        Administrador = 1,

        [Description("Professor")]
        Docente = 2,

        [Description("Discente")]
        Discente = 3
    }
}