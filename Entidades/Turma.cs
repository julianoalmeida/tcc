using System.Collections.Generic;

namespace Entidades
{
    public class Turma : Entidade
    {
        public Turma()
        {
            Discentes = new List<Discente>();
        }

        public virtual int Turno { get; set; }

        public virtual string TurnoToString
        {
            get
            {
                var turno = string.Empty;
                switch (this.Turno)
                {
                    case 1:
                        turno = "Matutino";
                        break;

                    case 2:
                        turno = "Vespertino";
                        break;
                    case 3:
                        turno = "Noturno";
                        break;
                }
                return turno;
            }
        }

        public virtual string Descricao { get; set; }

        public virtual Docente Docente { get; set; }

        public virtual IList<Discente> Discentes { get; set; }

        public virtual string IdsSelecionados { get; set; }

    }
}
