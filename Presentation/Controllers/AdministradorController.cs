using Comum;
using Entidades;
using Entidades.Enumeracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Negocio;

namespace Web.Controllers
{
    public class AdministradorController : BaseController
    {
        #region ATRIBUTOS

        private readonly IPessoaBusiness _servicoPessoa;
        private readonly IAdministradorBusiness _servicoAdministrador;
        private readonly IUsuarioBusiness _servicoUsuario;
        private readonly ICidadeBusiness _servicoCidade;
        private readonly IEstadoBusiness _servicoEstado;

        #endregion

        #region CONSTRUTOR

        public AdministradorController(IPessoaBusiness pessoa,
            IAdministradorBusiness administrador, IUsuarioBusiness usuario, ICidadeBusiness cidade,
            IEstadoBusiness estado)
        {
            _servicoPessoa = pessoa;
            _servicoAdministrador = administrador;
            _servicoUsuario = usuario;
            _servicoCidade = cidade;
            _servicoEstado = estado;
        }
        #endregion

        #region ACTIONS

        [HttpGet]
        public ActionResult Index()
        {
            return View(Constants.INDEX);
        }

        [HttpGet]
        public ActionResult Manter(int? id)
        {
            var model = new Administrator();

            if (id.HasValue)
            {
                model = _servicoAdministrador.GetById(id.Value);
            }

            CarregarDropDowns(model);

            return View(Constants.MANTER, model);
        }

        #endregion

        #region METODOS AUXILIARES

        /// <summary>
        /// Método Responsavel por Popular as DropDownList com os valores cadastrados na base de dados
        /// </summary>
        /// <param name="model">Teacher Atual</param>
        private void CarregarDropDowns(Administrator model)
        {
            if (model.Id > 0)
            {
                ViewBag.EstadoCivil = ConvertEnumToListItem<MaritalStatusEnum>(model.Person.MaritalState.ToString());
                ViewBag.Sexo = ConvertEnumToListItem<SexEnum>(model.Person.Sex.ToString());

                var cidades = _servicoCidade.SelectWithFilter(a => a.State.Code.Equals(model.Person.Address.State)).ToList();

                ViewBag.Cidades = BuildListSelectListItemWith(cidades, "Name", "Id", model.Person.Address.CityId.ToString());

                var estados = _servicoEstado.GetAll().ToList();
                ViewBag.Estados = BuildListSelectListItemWith(estados, "Name", "Code", model.Person.Address.State);
            }
            else
            {
                ViewBag.EstadoCivil = ConvertEnumToListItem<MaritalStatusEnum>(string.Empty);

                ViewBag.Sexo = ConvertEnumToListItem<SexEnum>(string.Empty);

                ViewBag.Cidades = BuildListSelectListItemWith(new List<City>(), "Name", "Id");

                ViewBag.Estados = BuildListSelectListItemWith(_servicoEstado.GetAll().ToList(), "Name", "Code");
            }
        }

        #endregion

        #region REQUISICOES_AJAX

        public JsonResult ListarPaginado(string Nome)
        {
            var paginaAtual = Convert.ToInt32(Request.Params[Constants.START_PAGE]);

            var adm = new Administrator { Person = new Person { Name = Nome } };

            var retorno = _servicoAdministrador.SelectWithPagination(adm, paginaAtual);

            var totalRegistros = _servicoAdministrador.Total(adm);

            return BuildJsonObject(retorno, totalRegistros);
        }

        public JsonResult ListarCidades(string siglaEstado)
        {
            var retorno = _servicoCidade.SelectWithFilter(a => a.State.Code.Equals(siglaEstado)).ToList();
            retorno.ForEach(a => a.State = null);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult Salvar(Administrator administrator)
        {
            var retorno = SUCESSO;


            var login = GetFormatedUserLoginAndPassword(administrator.Person);
            var mensagem = administrator.Id == 0 ? Messages.MI001 + login : Messages.MI002 + login;

            try
            {
                var usuario = _servicoUsuario.SelectWithFilter(a => a.Person.Id == administrator.Person.Id).FirstOrDefault() ?? new User { Person = new Person() };
                BuildLoggedUser(administrator.Person, usuario, (int)AccessProfileEnum.Administrador);

                _servicoPessoa.ValidadePerson(administrator.Person);

                _servicoAdministrador.SaveAndReturn(administrator);
                usuario.Person = _servicoPessoa.GetById(administrator.Person.Id);
                _servicoUsuario.SaveAndReturn(usuario);
            }
            catch (Exception ex)
            {
                retorno = GetErrorType(retorno, ex, ref mensagem);
            }
            return Json(new { retorno, msg = mensagem, discenteID = administrator.Id });
        }



        public JsonResult Excluir(int id)
        {
            var sucesso = true;
            try
            {
                var administrador = _servicoAdministrador.GetById(id);
                var usuario = _servicoUsuario.SelectWithFilter(a => a.Person.Id == administrador.Person.Id).FirstOrDefault();
                _servicoUsuario.Remove(usuario.Id);
                _servicoAdministrador.Remove(id);
            }
            catch
            {
                sucesso = false;
            }

            return Json(new { retorno = sucesso });
        }

        #endregion
    }
}
