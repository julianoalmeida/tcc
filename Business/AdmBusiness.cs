using Data;
using Entidades;
using Negocio.BaseTypes;

namespace Negocio
{
    public interface IAdmBusiness : IBaseBusiness<Adm> { }

    public class AdmBusiness : BaseBusiness<Adm>, IAdmBusiness
    {
        private readonly IPersonBusiness _personBusiness;
        public AdmBusiness(IAdmData repository, IPersonBusiness personBusiness)
            : base(repository)
        {
            _personBusiness = personBusiness;
        }

        public override void Validate(Adm entity)
        {
            _personBusiness.Validate(entity.Person);
        }
    }
}