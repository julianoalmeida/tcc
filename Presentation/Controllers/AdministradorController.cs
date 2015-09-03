using Comum;
using Comum.Contratos;
using Comum.Excecoes;
using Entidades;
using Entidades.Enumeracoes;
using Entidades.Extensions;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Web.Controllers
{
    public class AdministradorController : BaseController
    {
        #region ATRIBUTOS

        private IPessoaBusiness _servicoPessoa;
        private IAdministradorBusiness _servicoAdministrador;
        private IUsuarioBusiness _servicoUsuario;
        private ICidadeBusiness _servicoCidade;
        private IEstadoBusiness _servicoEstado;

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
            return View(Constantes.INDEX);
        }

        [HttpGet]
        public ActionResult Manter(int? id)
        {
            var model = new Administrador();

            if (id.HasValue)
            {
                model = _servicoAdministrador.ObterPorId(id.Value);
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
        private void CarregarDropDowns(Administrador model)
        {
            if (model.Id > 0)
            {
                ViewBag.EstadoCivil = ConverteEnumParaListItem<EstadoCivilEnum>(model.Pessoa.EstadoCivil.ToString(), true);
                ViewBag.Sexo = ConverteEnumParaListItem<SexoEnum>(model.Pessoa.Sexo.ToString(), true);

                var cidades = _servicoCidade.Listar().Where(a => a.Estado.Codigo.Equals(model.Pessoa.Endereco.CodigoUf)).ToList();
                ViewBag.Cidades = ConverteListItem(cidades, "Nome", "Id", true, model.Pessoa.Endereco.IdCidadeBrasil.ToString());

                var estados = _servicoEstado.Listar().ToList();
                ViewBag.Estados = ConverteListItem(estados, "Nome", "Codigo", true, model.Pessoa.Endereco.CodigoUf);
            }
            else
            {
                ViewBag.EstadoCivil = ConverteEnumParaListItem<EstadoCivilEnum>(string.Empty, true);
                ViewBag.Sexo = ConverteEnumParaListItem<SexoEnum>(string.Empty, true);
                ViewBag.Cidades = ConverteListItem(new List<Cidade>(), "Nome", "Id");
                ViewBag.Estados = ConverteListItem(_servicoEstado.Listar().ToList(), "Nome", "Codigo");
            }
        }

        #endregion

        #region REQUISICOES_AJAX

        public JsonResult ListarPaginado(string Nome)
        {
            var paginaAtual = Convert.ToInt32(Request.Params[Constantes.PAGINA_ATUAL]);

            var adm = new Administrador { Pessoa = new Pessoa { Nome = Nome } };

            var retorno = _servicoAdministrador.ListarTodos(adm, paginaAtual);

            var totalRegistros = _servicoAdministrador.TotalRegistros(adm);

            return RetornaJson(retorno, totalRegistros);
        }

        public JsonResult ListarCidades(string siglaEstado)
        {
            var retorno = _servicoCidade.Listar().Where(a => a.Estado.Codigo.Equals(siglaEstado)).ToList();
            retorno.ForEach(a => a.Estado = null);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult Salvar(Administrador administrador)
        {
            var retorno = SUCESSO;
            

            var login = RecuperarLoginSistema(administrador.Pessoa);
            var mensagem = administrador.Id == 0 ? Mensagens.MI001 + login : Mensagens.MI002 + login;

            try
            {
                var usuario = _servicoUsuario.Pesquisar(a => a.Pessoa.Id == administrador.Pessoa.Id).FirstOrDefault() ?? new Usuario { Pessoa = new Pessoa() };
                RecuperarUsuario(administrador.Pessoa, usuario, (int)PerfilAcessoEnum.Administrador);

                if (_servicoPessoa.validarPessoa(administrador.Pessoa))
                {
                    _servicoAdministrador.Salvar(administrador);
                    usuario.Pessoa = _servicoPessoa.ObterPorId(administrador.Pessoa.Id);
                    _servicoUsuario.Salvar(usuario);
                }
            }
            catch (Exception ex)
            {
                retorno = RecuperarTipoDeErro(retorno, ex, ref  mensagem);
            }
            return Json(new { retorno = retorno, msg = mensagem, discenteID = administrador.Id });
        }



        public JsonResult Excluir(int id)
        {
            var sucesso = true;
            try
            {
                var administrador = _servicoAdministrador.ObterPorId(id);
                var usuario = _servicoUsuario.Pesquisar(a => a.Pessoa.Id == administrador.Pessoa.Id).FirstOrDefault();
                _servicoUsuario.Excluir(usuario.Id);
                _servicoAdministrador.Excluir(id);
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
