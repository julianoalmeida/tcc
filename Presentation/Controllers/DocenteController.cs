using Comum;
using Comum.Contratos;
using Comum.Excecoes;
using Entidades;
using Entidades.Enumeracoes;
using Entidades.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DocenteController : BaseController
    {
        #region ATRIBUTOS

        private IPessoaBusiness _servicoPessoa;
        private IDocenteBusiness _servicoDocente;
        private IUsuarioBusiness _servicoUsuario;
        private ICidadeBusiness _servicoCidade;
        private IEstadoBusiness _servicoEstado;
        private IDisciplinaBusiness _servicoDisciplina;
        private const string DISCIPLINA = "Disciplinas";


        #endregion

        #region CONSTRUTOR

        public DocenteController(IPessoaBusiness pessoa,
            IDocenteBusiness docente, IDisciplinaBusiness disciplina, IUsuarioBusiness usuario, ICidadeBusiness cidade,
            IEstadoBusiness estado, IDisciplinaBusiness disciplinas)
        {
            _servicoPessoa = pessoa;
            _servicoDocente = docente;
            _servicoUsuario = usuario;
            _servicoCidade = cidade;
            _servicoEstado = estado;
            _servicoDisciplina = disciplinas;
        }
        #endregion

        #region ACTIONS

        [HttpGet]
        public ActionResult Index()
        {
            return View(Constantes.INDEX);
        }

        [HttpGet]
        public ActionResult Manter(int? id)
        {
            Docente model;

            if (id.HasValue)
            {
                model = _servicoDocente.ObterPorId(id.Value);
            }
            else
            {
                model = new Docente { Pessoa = new Pessoa() };
            }

            var disciplinas = model.Disciplinas;

            TempData[DISCIPLINA] = disciplinas.ToList();
            keepTempData();

            CarregarDropDowns(model);

            return View(Constantes.MANTER, model);
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
        /// <param name="model">Docente Atual</param>
        private void CarregarDropDowns(Docente model)
        {
            ViewBag.EstadoCivil = ConverteEnumParaListItem<EstadoCivilEnum>(model.Pessoa.EstadoCivil.ToString(), true);
            ViewBag.Sexo = ConverteEnumParaListItem<SexoEnum>(model.Pessoa.Sexo.ToString(), true);
            ViewBag.Escolaridades = ConverteEnumParaListItem<EscolaridadeEnum>(model.Escolaridade.ToString(), true);

            var disciplinas = _servicoDisciplina.Listar().ToList();
            ViewBag.Disciplinas = ConverteListItem(disciplinas, "Descricao", "Id");

            if (model.Id > 0)
            {
                var cidades = _servicoCidade.Listar().Where(a => a.Estado.Codigo.Equals(model.Pessoa.Endereco.CodigoUf)).ToList();
                ViewBag.Cidades = ConverteListItem(cidades, "Nome", "Id", true, model.Pessoa.Endereco.IdCidadeBrasil.ToString());

                var estados = _servicoEstado.Listar().ToList();
                ViewBag.Estados = ConverteListItem(estados, "Nome", "Codigo", true, model.Pessoa.Endereco.CodigoUf);
            }
            else
            {
                ViewBag.Cidades = ConverteListItem(new List<Cidade>(), "Descricao", "Id");
                ViewBag.Estados = ConverteListItem(_servicoEstado.Listar().ToList(), "Nome", "Codigo");
            }

        }

        #endregion

        #region REQUISICOES_AJAX

        public JsonResult ListarPaginado(string Nome)
        {
            var paginaAtual = Convert.ToInt32(Request.Params[Constantes.PAGINA_ATUAL]);

            var docente = new Docente { Pessoa = new Pessoa { Nome = Nome } };

            var docentes = _servicoDocente.ListarTodos(docente, paginaAtual);

            var totalRegistros = _servicoDocente.TotalRegistros(docente);

            docentes.ForEach(a => a.Disciplinas = null);

            return RetornaJson(docentes, totalRegistros);
        }

        public JsonResult ListarDisciplinas()
        {
            var disciplinas = TempData[DISCIPLINA] as List<Disciplina> ?? new List<Disciplina>();

            keepTempData();

            disciplinas.ForEach(a => a.Docentes = null);

            return RetornaJson(disciplinas, disciplinas.Count);
        }

        public JsonResult AdicionarDisciplina(int idDIsciplina)
        {
            var disciplina = _servicoDisciplina.ObterPorId(idDIsciplina);

            var disciplinas = TempData[DISCIPLINA] as List<Disciplina> ?? new List<Disciplina>();

            var duplicado = disciplinas.Any(a => a.Id == idDIsciplina);

            if (!duplicado)
            {
                disciplinas.Add(disciplina);
                TempData[DISCIPLINA] = disciplinas.OrderBy(a => a.Descricao).ToList();
            }

            keepTempData();

            return Json(new { duplicado = duplicado });
        }

        public JsonResult ListarCidades(string siglaEstado)
        {
            var retorno = _servicoCidade.Listar().Where(a => a.Estado.Codigo.Equals(siglaEstado)).ToList();
            retorno.ForEach(a => a.Estado = null);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult Salvar(Docente docente)
        {
            var retorno = 1;

            var login = RecuperarLoginSistema(docente.Pessoa);
            var mensagem = docente.Id == 0 ? Mensagens.MI001 + login : Mensagens.MI002 + login;

            try
            {
                var disciplinas = TempData[DISCIPLINA] as List<Disciplina>;
                foreach (var item in disciplinas)
                {
                    docente.Disciplinas.Add(_servicoDisciplina.ObterPorId(item.Id));
                }

                var usuario = _servicoUsuario.Pesquisar(a => a.Pessoa.Id == docente.Pessoa.Id).FirstOrDefault() ?? new Usuario { Pessoa = new Pessoa() };
                RecuperarUsuario(docente.Pessoa, usuario, (int)PerfilAcessoEnum.Docente);

                if (_servicoPessoa.validarPessoa(docente.Pessoa) && _servicoDocente.VerificarPreenchimentoCamposObrigatorios(docente))
                {
                    _servicoDocente.Salvar(docente);
                    usuario.Pessoa = _servicoPessoa.ObterPorId(docente.Pessoa.Id);
                    _servicoUsuario.Salvar(usuario);
                }
            }
            catch (Exception ex)
            {
                retorno = RecuperarTipoDeErro(retorno, ex, ref mensagem);
            }

            return Json(new { retorno = retorno, msg = mensagem, docenteID = docente.Id });
        }

        public JsonResult Excluir(int id)
        {

            var sucesso = true;
            try
            {
                var docente = _servicoDocente.ObterPorId(id);
                var usuario = _servicoUsuario.Pesquisar(a => a.Pessoa.Id == docente.Pessoa.Id).FirstOrDefault();
                _servicoUsuario.Excluir(usuario.Id);
                _servicoDocente.Excluir(id);
            }
            catch
            {
                sucesso = false;
            }

            return Json(new { retorno = sucesso });
        }

        public JsonResult ExcluirDisciplina(int idDisciplina)
        {
            var disciplinas = TempData[DISCIPLINA] as List<Disciplina> ?? new List<Disciplina>();
            disciplinas.RemoveAll(a => a.Id == idDisciplina);
            TempData[DISCIPLINA] = disciplinas;
            keepTempData();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
