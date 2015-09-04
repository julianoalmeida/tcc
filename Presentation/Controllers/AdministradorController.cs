using Comum;
using Entidades;
using Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Negocio;
using NHibernate.Util;

namespace Web.Controllers
{
    [ValidateInput(false)]
    public class AdministradorController : BaseController
    {
        private readonly IPessoaBusiness _personBusiness;
        private readonly IAdministradorBusiness _admBusiness;
        private readonly IUsuarioBusiness _usesBusiness;
        private readonly ICidadeBusiness _cityBusiness;
        private readonly IEstadoBusiness _stateBusiness;

        public AdministradorController(IPessoaBusiness pessoa,
            IAdministradorBusiness administrador, IUsuarioBusiness usuario, ICidadeBusiness cidade,
            IEstadoBusiness estado)
        {
            _personBusiness = pessoa;
            _admBusiness = administrador;
            _usesBusiness = usuario;
            _cityBusiness = cidade;
            _stateBusiness = estado;
        }

        [HttpGet]
        public new ActionResult Index() => View();

        [HttpGet]
        public ActionResult Manter(int? id)
        {
            var model = id.HasValue ? _admBusiness.GetById(id.Value) : new Administrator();

            BuildDropDownLists(model);

            return View(model);
        }

        public JsonResult ListarPaginado(string Nome)
        {
            var paginaAtual = int.Parse(Request.Params[Constants.START_PAGE]);

            var adm = new Administrator { Person = new Person { Name = Nome } };

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

        public JsonResult Salvar(Administrator administrator)
        {
            var mensagem = GetSuccessMensagemForSaveOrUpdate(administrator.Id);

            try
            {
                ValidateAndSave(administrator);
            }
            catch (Exception ex)
            {
                mensagem = GetErrorMessageFromExceptionType(ex);
            }

            return Json(new { mensagem, administradorId = administrator.Id });
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

        private void ValidateAndSave(Administrator administrator)
        {
            _personBusiness.ValidadePerson(administrator.Person);
            _admBusiness.SaveAndReturn(administrator);
        }

        private void BuildDropDownLists(Administrator model)
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