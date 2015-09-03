using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entidades.Extensions;

namespace Entidades
{

    public class Pessoa : Entidade
    {
        public virtual Endereco Endereco { get; set; }

        private string _nome;
        public virtual string Nome
        {
            get { return _nome ?? (_nome = string.Empty); }
            set { _nome = value; }
        }

        private string _cpf;
        public virtual string Cpf
        {
            get { return _cpf == null ? string.Empty : _cpf.RemoveCaracteresMascara(); }
            set { _cpf = value; }
        }

        public virtual int Sexo { get; set; }

        public virtual DateTime? DataNascimento { get; set; }

        public virtual string Email { get; set; }

        private string _telefone;
        public virtual string Telefone
        {
            get { return _telefone == null ? string.Empty : _telefone.RemoveCaracteresMascara(); }
            set { _telefone = value; }
        }

        private string _celular;
        public virtual string Celular
        {
            get { return _celular == null ? string.Empty : _celular.RemoveCaracteresMascara(); }
            set { _celular = value; }
        }

        public virtual string Observacao { get; set; }

        public virtual int EstadoCivil { get; set; }
    }
}
