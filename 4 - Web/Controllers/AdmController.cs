using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Comum;
using Entidades;
using Entidades.Enums;
using Negocio;
using NHibernate.Util;

namespace _4___Web.Controllers
{   
    public class AdmController : BaseController
    {
        private readonly IPersonBusiness _personBusiness;
        private readonly IAdmBusiness _admBusiness;
        private readonly IUserBusiness _usesBusiness;
        private readonly ICityBusiness _cityBusiness;
        private readonly IStateBusiness _stateBusiness;

        public AdmController(IPersonBusiness person,
            IAdmBusiness adm, IUserBusiness user, ICityBusiness city,
            IStateBusiness state)
        {
            _personBusiness = person;
            _admBusiness = adm;
            _usesBusiness = user;
            _cityBusiness = city;
            _stateBusiness = state;
        }

        [HttpGet]
        public new ActionResult Index() => View();

        [HttpGet]
        public ActionResult Manter(int? id)
        {
            var model = id.HasValue ? _admBusiness.GetById(id.Value) : new Adm();

            BuildDropDownLists(model);

            return View(model);
        }

        public JsonResult ListarPaginado(string Nome)
        {
            var paginaAtual = int.Parse(Request.Params[Constants.START_PAGE]);

            var adm = new Adm { Person = new Person { Name = Nome } };

            var retorno = _admBusiness.SelectWithPagination(adm, paginaAtual);

            var totalRegistros = _admBusiness.Total(adm);

            return BuildJsonObject(retorno, totalRegistros);
        }

        public JsonResult ListarCidades(string siglaEstado)
        {
            var cities = _cityBusiness.SelectWithFilter(a => a.State.Code.Equals(siglaEstado));

            cities.ForEach(a => a.State = null);

            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Salvar(Adm adm)
        {
            var mensagem = GetSuccessMensagemForSaveOrUpdate(adm.Id);

            try
            {
                ValidateAndSave(adm);
            }
            catch (Exception ex)
            {
                mensagem = GetErrorMessageFromExceptionType(ex);
            }

            return Json(new { mensagem, administradorId = adm.Id });
        }

        public JsonResult Excluir(int id)
        {
            var sucesso = true;
            try
            {
                var administrador = _admBusiness.GetById(id);
                var usuario = _usesBusiness.SelectWithFilter(a => a.Person.Id == administrador.Person.Id).FirstOrDefault();
                _usesBusiness.Remove(usuario.Id);
                _admBusiness.Remove(id);
            }
            catch
            {
                sucesso = false;
            }

            return Json(new { retorno = sucesso });
        }

        private void ValidateAndSave(Adm adm)
        {
            _personBusiness.ValidadePerson(adm.Person);
            _admBusiness.SaveAndReturn(adm);
        }

        private void BuildDropDownLists(Adm model)
        {
            ViewBag.EstadoCivil = BuildListItemfromEnum<MaritalStatusEnum>(model.Person.MaritalState.ToString());
            ViewBag.Sexo = BuildListItemfromEnum<SexEnum>(model.Person.Sex.ToString());

            ViewBag.Cidades = model.Id > 0
                ? BuildListSelectListItemWith(
                    _cityBusiness.SelectWithFilter(a => a.State.Code.Equals(model.Person.Address.State)), "Name", "Id",
                    model.Person.Address.CityId.ToString())
                : BuildListSelectListItemWith(new List<City>(), "Name", "Id");

            ViewBag.Estados = BuildListSelectListItemWith(_stateBusiness.GetAll().ToList(), "Name", "Code");
        }
    }
}