using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Comum;
using Entidades;
using Entidades.Enums;
using Negocio;

namespace _4___Web.Controllers
{
    public class TeacherController : BaseController
    {
        #region ATRIBUTOS

        private readonly IPersonBusiness _servicoPerson;
        private readonly ITeacherBusiness _servicoTeacher;
        private readonly IUserBusiness _servicoUser;
        private readonly ICityBusiness _servicoCity;
        private readonly IStateBusiness _servicoState;
        private readonly ICourseBusiness _servicoCourse;
        private const string DISCIPLINA = "Courses";

        #endregion

        #region CONSTRUTOR

        public TeacherController(IPersonBusiness person,
            ITeacherBusiness teacher, ICourseBusiness course, IUserBusiness user, ICityBusiness city,
            IStateBusiness state, ICourseBusiness courses)
        {
            _servicoPerson = person;
            _servicoTeacher = teacher;
            _servicoUser = user;
            _servicoCity = city;
            _servicoState = state;
            _servicoCourse = courses;
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
            Teacher model;

            if (id.HasValue)
            {
                model = _servicoTeacher.GetById(id.Value);
            }
            else
            {
                model = new Teacher { Person = new Person() };
            }

            var disciplinas = model.Courses;

            TempData[DISCIPLINA] = disciplinas.ToList();
            keepTempData();

            CarregarDropDowns(model);

            return View(Constants.MANTER, model);
        }

        #endregion

        #region METODOS AUXILIARES

        private void keepTempData()
        {
            TempData.Keep(DISCIPLINA);
        }


        /// <summary>
        /// Método Responsavel por Popular as DropDownList com os valores cadastrados na base de dados
        /// </summary>
        /// <param name="model">Teacher Atual</param>
        private void CarregarDropDowns(Teacher model)
        {
            ViewBag.EstadoCivil = BuildListItemfromEnum<MaritalStatusEnum>(model.Person.MaritalState.ToString());
            ViewBag.Sexo = BuildListItemfromEnum<SexEnum>(model.Person.Sex.ToString());
            ViewBag.Escolaridades = BuildListItemfromEnum<EducationEnum>(model.Education.ToString());

            var disciplinas = _servicoCourse.GetAll();
            ViewBag.Disciplinas = BuildListSelectListItemWith(disciplinas, "Description", "Id");

            if (model.Id > 0)
            {
                var cidades = _servicoCity.SelectWithFilter(a => a.State.Code.Equals(model.Person.Address.State)).ToList();
                ViewBag.Cidades = BuildListSelectListItemWith(cidades, "Name", "Id", model.Person.Address.CityId.ToString());

                var estados = _servicoState.GetAll().ToList();
                ViewBag.Estados = BuildListSelectListItemWith(estados, "Name", "Code", model.Person.Address.State);
            }
            else
            {
                ViewBag.Cidades = BuildListSelectListItemWith(new List<City>(), "Description", "Id");
                ViewBag.Estados = BuildListSelectListItemWith(_servicoState.GetAll(), "Name", "Code");
            }

        }

        #endregion

        #region REQUISICOES_AJAX

        public JsonResult ListarPaginado(string Nome)
        {
            var paginaAtual = Convert.ToInt32(Request.Params[Constants.START_PAGE]);

            var docente = new Teacher { Person = new Person { Name = Nome } };

            var docentes = _servicoTeacher.SelectWithPagination(docente, paginaAtual);

            var totalRegistros = _servicoTeacher.Total(docente);

            docentes.ForEach(a => a.Courses = null);

            return BuildJsonObject(docentes, totalRegistros);
        }

        public JsonResult ListarDisciplinas()
        {
            var disciplinas = TempData[DISCIPLINA] as List<Courses> ?? new List<Courses>();

            keepTempData();

            disciplinas.ForEach(a => a.Teachers = null);

            return BuildJsonObject(disciplinas, disciplinas.Count);
        }

        public JsonResult AdicionarDisciplina(int idDIsciplina)
        {
            var disciplina = _servicoCourse.GetById(idDIsciplina);

            var disciplinas = TempData[DISCIPLINA] as List<Courses> ?? new List<Courses>();

            var duplicado = disciplinas.Any(a => a.Id == idDIsciplina);

            if (!duplicado)
            {
                disciplinas.Add(disciplina);
                TempData[DISCIPLINA] = disciplinas.OrderBy(a => a.Description).ToList();
            }

            keepTempData();

            return Json(new { duplicado = duplicado });
        }

        public JsonResult ListarCidades(string siglaEstado)
        {
            var retorno = _servicoCity.SelectWithFilter(a => a.State.Code.Equals(siglaEstado)).ToList();
            retorno.ForEach(a => a.State = null);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult Salvar(Teacher teacher)
        {
            var retorno = 1;

            var login = GetFormatedUserLoginAndPassword(teacher.Person);
            var mensagem = teacher.Id == 0 ? Messages.SUCCESSFULLY_INSERTED_RECORD + login : Messages.SUCCESSFULLY_UPDATED_RECORD + login;

            try
            {
                var disciplinas = TempData[DISCIPLINA] as List<Courses>;
                foreach (var item in disciplinas)
                {
                    teacher.Courses.Add(_servicoCourse.GetById(item.Id));
                }

                var usuario = _servicoUser.SelectWithFilter(a => a.Person.Id == teacher.Person.Id).FirstOrDefault() ?? new User { Person = new Person() };
                BuildLoggedUser(teacher.Person, usuario, (int)AccessProfileEnum.Docente);

                _servicoPerson.ValidadePerson(teacher.Person);

                if (_servicoTeacher.IsRequiredFieldsFilled(teacher))
                {
                    _servicoTeacher.SaveAndReturn(teacher);
                    usuario.Person = _servicoPerson.GetById(teacher.Person.Id);
                    _servicoUser.SaveAndReturn(usuario);
                }
            }
            catch (Exception ex)
            {
                mensagem = GetErrorMessageFromExceptionType(ex);
            }

            return Json(new { retorno, msg = mensagem, docenteID = teacher.Id });
        }

        public JsonResult Excluir(int id)
        {
            var sucesso = true;
            try
            {
                var docente = _servicoTeacher.GetById(id);
                var usuario = _servicoUser.SelectWithFilter(a => a.Person.Id == docente.Person.Id).FirstOrDefault();
                _servicoUser.Remove(usuario.Id);
                _servicoTeacher.Remove(id);
            }
            catch
            {
                sucesso = false;
            }

            return Json(new { retorno = sucesso });
        }

        public JsonResult ExcluirDisciplina(int idDisciplina)
        {
            var disciplinas = TempData[DISCIPLINA] as List<Courses> ?? new List<Courses>();
            disciplinas.RemoveAll(a => a.Id == idDisciplina);
            TempData[DISCIPLINA] = disciplinas;
            keepTempData();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
