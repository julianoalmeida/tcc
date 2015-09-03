using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Entidades
{
    public class Cidade : Entidade
    {
        public virtual string Nome { get; set; }

        public virtual Estado Estado { get; set; }
    }
}
