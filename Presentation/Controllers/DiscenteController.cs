using Comum;
using Comum.Contratos;
using Comum.Excecoes;
using Entidades;
using Entidades.Enumeracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DiscenteController : BaseController
    {
        #region INJEÇÃO

        private IPessoaBusiness _servicoPessoa;
        private IDiscenteBusiness _servicoDiscente;
        private IUsuarioBusiness _servicoUsuario;
        private ICidadeBusiness _servicoCidade;
        private IEstadoBusiness _servicoEstado;

        #endregion

        #region CONSTRUTOR

        public DiscenteController(IPessoaBusiness pessoa,
            IDiscenteBusiness discente, IDisciplinaBusiness disciplina, IUsuarioBusiness usuario, ICidadeBusiness cidade,
            IEstadoBusiness estado)
        {
            _servicoPessoa = pessoa;
            _servicoDiscente = discente;
            _servicoUsuario = usuario;
            _servicoCidade = cidade;
            _servicoEstado = estado;
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
            var model = new Discente();

            if (id.HasValue)
            {
                model = _servicoDiscente.ObterPorId(id.Value);
            }

            CarregarDropDowns(model);

            return View(Constantes.MANTER, model);
        }

        #endregion

        #region METODOS AUXILIARES

        /// <summary>
        /// Método Responsavel por Popular as DropDownList com os valores cadastrados na base de dados
        /// </summary>
        /// <param name="model">Docente Atual</param>
        private void CarregarDropDowns(Discente model)
        {
            if (model.Id > 0)
            {
                ViewBag.EstadoCivil = ConverteEnumParaListItem<EstadoCivilEnum>(model.Pessoa.EstadoCivil.ToString(), true);
                ViewBag.Sexo = ConverteEnumParaListItem<SexoEnum>(model.Pessoa.Sexo.ToString(), true);


                var cidades = _servicoCidade.Listar().Where(a => a.Estado.Codigo.Equals(model.Pessoa.Endereco.CodigoUf)).ToList();
                ViewBag.Cidades = ConverteListItem(cidades, "Nome", "Id", true, model.Pessoa.Endereco.IdCidadeBrasil.ToString());

                var estados = _servicoEstado.Listar().ToList();
                ViewBag.Estados = ConverteListItem(estados, "Nome", "Codigo", true, model.Pessoa.Endereco.CodigoUf);

                ViewBag.Escolaridades = ConverteEnumParaListItem<EscolaridadeEnum>(model.Escolaridade.ToString(), true);
            }
            else
            {
                ViewBag.EstadoCivil = ConverteEnumParaListItem<EstadoCivilEnum>(string.Empty, true);
                ViewBag.Sexo = ConverteEnumParaListItem<SexoEnum>(string.Empty, true);
                ViewBag.Cidades = ConverteListItem(new List<Cidade>(), "Descricao", "Id");
                ViewBag.Estados = ConverteListItem(_servicoEstado.Listar().ToList(), "Nome", "Codigo");
                ViewBag.Escolaridades = ConverteEnumParaListItem<EscolaridadeEnum>(model.Escolaridade.ToString(), true);
            }
        }

        #endregion

        #region REQUISICOES_AJAX

        public JsonResult ListarPaginado(string Nome)
        {
            var paginaAtual = Convert.ToInt32(Request.Params[Constantes.PAGINA_ATUAL]);

            var discente = new Discente { Pessoa = new Pessoa { Nome = Nome } };

            var discentes = _servicoDiscente.ListarTodos(discente, paginaAtual);

            var totalRegistros = _servicoDiscente.TotalRegistros(discente);

            discentes.ForEach(a => a.Turmas = null);
            return RetornaJson(discentes, totalRegistros);
        }

        public JsonResult ListarCidades(string siglaEstado)
        {
            var retorno = _servicoCidade.Listar().Where(a => a.Estado.Codigo.Equals(siglaEstado)).ToList();
            retorno.ForEach(a => a.Estado = null);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult Salvar(Discente discente)
        {
            var retorno = SUCESSO;

            var login = RecuperarLoginSistema(discente.Pessoa);
            var mensagem = discente.Id == 0 ? Mensagens.MI001 + login : Mensagens.MI002 + login;

            try
            {
                var usuario = _servicoUsuario.Pesquisar(a => a.Pessoa.Id == discente.Pessoa.Id).FirstOrDefault() ?? new Usuario { Pessoa = new Pessoa() };
                RecuperarUsuario(discente.Pessoa, usuario, (int)PerfilAcessoEnum.Discente);

                if (_servicoPessoa.validarPessoa(discente.Pessoa) && _servicoDiscente.VerificarPreenchimentoEscolaridade(discente))
                {
                    discente.Matricula = _servicoDiscente.GerarNumeroMatricula(discente);
                    _servicoDiscente.Salvar(discente);
                    usuario.Pessoa = _servicoPessoa.ObterPorId(discente.Pessoa.Id);
                    _servicoUsuario.Salvar(usuario);
                }
            }
            catch (Exception ex)
            {
                retorno = RecuperarTipoDeErro(retorno, ex, ref mensagem);
            }

            return Json(new { retorno = retorno, msg = mensagem, discenteID = discente.Id });
        }

        public JsonResult Excluir(int id)
        {

            var sucesso = true;
            try
            {
                var discente = _servicoDiscente.ObterPorId(id);
                var usuario = _servicoUsuario.Pesquisar(a => a.Pessoa.Id == discente.Pessoa.Id).FirstOrDefault();
                _servicoUsuario.Excluir(usuario.Id);
                _servicoDiscente.Excluir(id);
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
