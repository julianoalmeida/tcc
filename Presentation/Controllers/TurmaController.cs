using Comum;
using Entidades;
using Entidades.Enums;
using System;
using System.Web.Mvc;
using System.Linq;
using Comum.Exceptions;
using Negocio;
using NHibernate.Util;

namespace Web.Controllers
{
    public class TurmaController : BaseController
    {
        private readonly ITurmaBusiness _servicoTurma;
        private readonly IDiscenteBusiness _servicoDiscente;
        private readonly IDocenteBusiness _servicoDocente;

        public TurmaController(ITurmaBusiness negocio, IDiscenteBusiness discente, IDocenteBusiness docente)
        {
            _servicoTurma = negocio;
            _servicoDiscente = discente;
            _servicoDocente = docente;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Turno = BuildListItemfromEnum<ClassesTimeEnum>(string.Empty);

            return View();
        }

        [HttpGet]
        public ActionResult Manter(int? id)
        {
            var turma = id.HasValue ? _servicoTurma.GetById(id.Value) : new Class();

            BuildDropDownLists(turma);

            return View(turma);
        }

        private void GetSelectedDiscents(Class model)
        {
            model.SelectedStudentsId.Split(',')
                .ForEach(idDiscente => model.Students.Add(_servicoDiscente.GetById(int.Parse(idDiscente))));
        }

        private void BuildDropDownLists(Class model)
        {
            ViewBag.Turno = BuildListItemfromEnum<ClassesTimeEnum>(model.ClassTime.ToString());

            var docentes = _servicoDocente.GetAll().ToList();
            ViewBag.Docentes = BuildListSelectListItemWith(docentes, "Person.Name", "Id");

            var discentesSelecionados = model.Students.ToList();

            var todosDiscentesCadastrados = _servicoDiscente.GetAll().ToList();

            discentesSelecionados
                .ForEach(
                    discente =>
                        todosDiscentesCadastrados.RemoveAll(discenteCadastrado => discente.Id == discenteCadastrado.Id));

            ViewBag.DiscentesNaoSelecionados = BuildListSelectListItemWith(todosDiscentesCadastrados, "Person.Name", "Id");

            ViewBag.DiscentesSelecionados = BuildListSelectListItemWith(discentesSelecionados, "Person.Name", "Id");
        }

        public JsonResult ListarPaginado(string nome, int? turno)
        {
            var turma = new Class { Description = nome, ClassTime = turno.HasValue ? turno.Value : 0 };

            var paginaAtual = Convert.ToInt32(Request.Params[Constants.START_PAGE]);

            var retorno = _servicoTurma.SelectWithPagination(turma, paginaAtual);

            foreach (var item in retorno)
            {
                item.Teacher = null;
                item.Students = null;
            }

            return BuildJsonObject(retorno, retorno.Count);
        }

        public JsonResult Salvar(Class model)
        {
            var jsonResult = new JsonResult();
            try
            {
                GetSelectedDiscents(model);
                model.Teacher = _servicoDocente.GetById(model.Teacher.Id);

                _servicoTurma.ValidateTurmaBusinessRules(model);
                _servicoTurma.SaveAndReturn(model);
            }
            catch (Exception e)
            {
                jsonResult = e.GetType() == typeof(DuplicatedEntityException)
                    ? BuildJson(0, Messages.MI009)
                    : BuildJson(0, Messages.MI008);
            }

            return jsonResult;
        }

        private JsonResult BuildJson(int retorno, string msg)
        {
            return Json(new { sucesso = retorno, msg });
        }

        public JsonResult Excluir(int id)
        {
            var sucesso = true;
            try
            {
                var turma = _servicoTurma.GetById(id);
                turma.Students.Clear();
                _servicoTurma.SaveAndReturn(turma);

                _servicoTurma.Remove(id);
            }
            catch
            {
                sucesso = false;
            }

            return Json(sucesso);
        }
    }
}
