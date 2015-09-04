using System.Collections.Generic;
using Data;
using Entidades;

namespace Negocio
{
    public interface IAdmBusiness : INegocioBase<Adm>
    {
        int Total(Adm adm);
        List<Adm> SelectWithPagination(Adm adm, int startPage);
    }

    public class AdmBusiness : BaseBusiness<Adm>, IAdmBusiness
    {
        private readonly IAdmData _admData;
        public AdmBusiness(IAdmData admData)
            : base(admData)
        {
            _admData = admData;
        }

        public int Total(Adm adm)
        {
            return _admData.Total(adm);
        }

        public List<Adm> SelectWithPagination(Adm adm, int startPage)
        {
            return _admData.SelectWithPagination(adm, startPage);
        }
    }
}