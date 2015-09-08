using Comum;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Entidades.Enums;
using Negocio;

namespace Web.Controllers
{
    public class DiscenteController : BaseController
    {
        #region INJEÇÃO

        private IPersonBusiness _servicoPerson;
        private IStudentBusiness _servicoStudent;
        private IUserBusiness _servicoUser;
        private ICityBusiness _servicoCity;
        private IStateBusiness _servicoState;

        #endregion

        #region CONSTRUTOR

        public DiscenteController(IPersonBusiness person,
            IStudentBusiness student, ICourseBusiness course, IUserBusiness user, ICityBusiness city,
            IStateBusiness state)
        {
            _servicoPerson = person;
            _servicoStudent = student;
            _servicoUser = user;
            _servicoCity = city;
            _servicoState = state;
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
            var model = new Student();

            if (id.HasValue)
            {
                model = _servicoStudent.GetById(id.Value);
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
        private void CarregarDropDowns(Student model)
        {
            if (model.Id > 0)
            {
                ViewBag.EstadoCivil = BuildListItemfromEnum<MaritalStatusEnum>(model.Person.MaritalState.ToString());
                ViewBag.Sexo = BuildListItemfromEnum<SexEnum>(model.Person.Sex.ToString());

                var cidades = _servicoCity.SelectWithFilter(a => a.State.Code.Equals(model.Person.Address.State)).ToList();
                ViewBag.Cidades = BuildListSelectListItemWith(cidades, "Name", "Id", model.Person.Address.CityId.ToString());

                var estados = _servicoState.GetAll().ToList();
                ViewBag.Estados = BuildListSelectListItemWith(estados, "Name", "Code", model.Person.Address.State);

                ViewBag.Escolaridades = BuildListItemfromEnum<EducationEnum>(model.Education.ToString());
            }
            else
            {
                ViewBag.EstadoCivil = BuildListItemfromEnum<MaritalStatusEnum>(string.Empty);
                ViewBag.Sexo = BuildListItemfromEnum<SexEnum>(string.Empty);
                ViewBag.Cidades = BuildListSelectListItemWith(new List<City>(), "Description", "Id");
                ViewBag.Estados = BuildListSelectListItemWith(_servicoState.GetAll().ToList(), "Name", "Code");
                ViewBag.Escolaridades = BuildListItemfromEnum<EducationEnum>(model.Education.ToString());
            }
        }

        #endregion

        #region REQUISICOES_AJAX

        public JsonResult ListarPaginado(string Nome)
        {
            var paginaAtual = Convert.ToInt32(Request.Params[Constants.START_PAGE]);

            var discente = new Student { Person = new Person { Name = Nome } };

            var discentes = _servicoStudent.SelectWithPagination(discente, paginaAtual);

            var totalRegistros = _servicoStudent.Total(discente);

            discentes.ForEach(a => a.Classes = null);
            return BuildJsonObject(discentes, totalRegistros);
        }

        public JsonResult ListarCidades(string siglaEstado)
        {
            var retorno = _servicoCity.SelectWithFilter(a => a.State.Code.Equals(siglaEstado)).ToList();
            retorno.ForEach(a => a.State = null);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult Salvar(Student student)
        {
            var login = GetFormatedUserLoginAndPassword(student.Person);
            var mensagem = student.Id == 0 ? Messages.SUCCESSFULLY_INSERTED_RECORD + login : Messages.SUCCESSFULLY_UPDATED_RECORD + login;

            try
            {
                var usuario = _servicoUser.SelectWithFilter(a => a.Person.Id == student.Person.Id).FirstOrDefault() ?? new User { Person = new Person() };
                BuildLoggedUser(student.Person, usuario, (int)AccessProfileEnum.Discente);

                _servicoPerson.ValidadePerson(student.Person);

                if (_servicoStudent.IsEducationFieldFilled(student))
                {
                    student.RegistrationNumber = _servicoStudent.BuildRegistrationNumber(student);
                    _servicoStudent.SaveAndReturn(student);
                    usuario.Person = _servicoPerson.GetById(student.Person.Id);
                    _servicoUser.SaveAndReturn(usuario);
                }
            }
            catch (Exception ex)
            {
                mensagem = GetErrorMessageFromExceptionType(ex);
            }

            return Json(new { mensagem, studentID = student.Id });
        }

        public JsonResult Excluir(int id)
        {

            var sucesso = true;
            try
            {
                var discente = _servicoStudent.GetById(id);
                var usuario = _servicoUser.SelectWithFilter(a => a.Person.Id == discente.Person.Id).FirstOrDefault();
                _servicoUser.Remove(usuario.Id);
                _servicoStudent.Remove(id);
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
