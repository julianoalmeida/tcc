using Comum;
using Comum.Contratos;
using Comum.Contratos.Turmas;
using Entidades;
using Entidades.Enumeracoes;
using System;
using System.Web.Mvc;
using System.Linq;
using Comum.Excecoes;

namespace Web.Controllers
{
    public class TurmaController : BaseController
    {
        #region Atributos


        private ITurmaBusiness _servicoTurma;
        private IDiscenteBusiness _servicoDiscente;
        private IDocenteBusiness _servicoDocente;

        #endregion

        #region Construtor

        public TurmaController(ITurmaBusiness negocio, IDiscenteBusiness discente, IDocenteBusiness docente)
        {
            _servicoTurma = negocio;
            _servicoDiscente = discente;
            _servicoDocente = docente;
        }

        #endregion

        #region Action

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Turno = ConverteEnumParaListItem<TurnoEnum>(string.Empty, true);

            return View();
        }

        [HttpGet]
        public ActionResult Manter(int? id)
        {
            var turma = id.HasValue ? _servicoTurma.ObterPorId(id.Value) : new Turma();

            CarregarDrops(turma);

            return View(turma);
        }

        #endregion

        #region Metodos Auxiliares

        private void RecuperarDiscentesVinculados(Turma model)
        {
            var idDiscentesVinculados = model.IdsSelecionados.Split(',');

            foreach (var idDiscente in idDiscentesVinculados)
            {
                model.Discentes.Add(_servicoDiscente.ObterPorId(int.Parse(idDiscente)));
            }
        }

        private void CarregarDrops(Turma model)
        {
            ViewBag.Turno = ConverteEnumParaListItem<TurnoEnum>(model.Turno.ToString(), true);

            var docentes = _servicoDocente.Listar().ToList();
            ViewBag.Docentes = ConverteListItem(docentes, "Pessoa.Nome", "Id");

            var discentesSelecionados = model.Discentes.ToList();

            var todosDiscentesCadastrados = _servicoDiscente.Listar().ToList();

            foreach (var item in discentesSelecionados)
            {
                todosDiscentesCadastrados.RemoveAll(a => a.Id == item.Id);
            }

            ViewBag.DiscentesNaoSelecionados = ConverteListItem(todosDiscentesCadastrados, "Pessoa.Nome", "Id", false);

            ViewBag.DiscentesSelecionados = ConverteListItem(discentesSelecionados, "Pessoa.Nome", "Id", false);

        }

        #endregion

        #region Ajax

        public JsonResult ListarPaginado(string nome, int? turno)
        {
            var turma = new Turma { Descricao = nome, Turno = turno.HasValue ? turno.Value : 0 };

            var paginaAtual = Convert.ToInt32(Request.Params[Constantes.PAGINA_ATUAL]);

            var retorno = _servicoTurma.ListarTodos(turma, turno, paginaAtual);

            foreach (var item in retorno)
            {
                item.Docente = null;
                item.Discentes = null;
            }

            return RetornaJson(retorno, retorno.Count);
        }

        public JsonResult Salvar(Turma model)
        {
            var retorno = SUCESSO;
            var msg = model.Id == 0 ? Mensagens.MI001 : Mensagens.MI002;

            try
            {
                RecuperarDiscentesVinculados(model);
                model.Docente = _servicoDocente.ObterPorId(model.Docente.Id);

                retorno = _servicoTurma.ValidarRegrasNegocio(model);

                if (retorno == SUCESSO)
                {
                    _servicoTurma.Salvar(model);
                }
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(RegistroDuplicadoException))
                {
                    retorno = REGISTRO_DUPLICADO;
                    msg = Mensagens.MI009;
                }
                else
                {
                    retorno = TOTAL_DISCENTES_MAIOR_QUANTIDADE_VAGAS;
                    msg = Mensagens.MI008;
                }

            }

            return Json(new { sucesso = retorno, msg = msg });
        }

        public JsonResult Excluir(int id)
        {
            var sucesso = true;
            try
            {
                var turma = _servicoTurma.ObterPorId(id);
                turma.Discentes.Clear();
                _servicoTurma.Salvar(turma);

                _servicoTurma.Excluir(id);
            }
            catch
            {
                sucesso = false;
            }

            return Json(sucesso);
        }

        #endregion

    }
}
