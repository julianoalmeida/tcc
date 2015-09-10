using System.Collections.Generic;
using Data;
using Entidades;

namespace Negocio
{
    public interface IAdmBusiness : IBaseBusiness<Adm>
    {
        int Total(Adm adm);
        List<Adm> SelectWithPagination(Adm adm, int startPage);
    }

    public class AdmBusinessBusiness : BaseBusinessBusiness<Adm>, IAdmBusiness
    {
        private readonly IAdmData _admData;
        private readonly IPersonBusiness _personBusiness;
        public AdmBusinessBusiness(IAdmData admData, IPersonBusiness personBusiness)
            : base(admData)
        {
            _admData = admData;
            _personBusiness = personBusiness;
        }

        public int Total(Adm adm)
        {
            return _admData.Total(adm);
        }

        public List<Adm> SelectWithPagination(Adm adm, int startPage)
        {
            return _admData.SelectWithPagination(adm, startPage);
        }

        public override void Validate(Adm entity)
        {
            _personBusiness.Validate(entity.Person);
        }
    }
}