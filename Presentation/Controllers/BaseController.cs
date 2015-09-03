using Comum;
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
    public class BaseController : Controller
    {
        public static int perfilLogado { get; set; }
        public static string NomeLogado { get; set; }

        public const int ERRO = 0;
        public const int SUCESSO = 1;
        public const int REGISTRO_DUPLICADO = 2;
        public const int CAMPO_OBRIGATORIO_NAO_INFORMADO = 3;
        public const int TOTAL_DISCENTES_MAIOR_QUANTIDADE_VAGAS = 4;
        public const int CPF_INVALIDO = 5;
        public const int EMAIL_INVALIDO = 6;
        public const int DATA_ATUAL_FUTURA = 7;


        public ActionResult Index()
        {
            var usuarioLogado = TempData[Constantes.USUARIO_LOGADO] as Usuario;

            perfilLogado = usuarioLogado.PerfilAcesso;
            NomeLogado = usuarioLogado.Nome;
            TempData.Keep(Constantes.USUARIO_LOGADO);

            return RedirectToAction("Index", "Home");
        }

        protected int RecuperarTipoDeErro(int retorno, Exception ex,ref  string msg)
        {
            if (ex.GetType() == typeof(DataAtualFuturaException))
            {
                retorno = DATA_ATUAL_FUTURA;
                msg = Mensagens.MI003;
            }
            else if (ex.GetType() == typeof(RegistroDuplicadoException))
            {
                retorno = REGISTRO_DUPLICADO;
                msg = Mensagens.MI009;
            }
            else if (ex.GetType() == typeof(CpfException))
            {
                retorno = CPF_INVALIDO;
                msg = Mensagens.MI004;

            }
            else if (ex.GetType() == typeof(EmailException))
            {
                retorno = EMAIL_INVALIDO;
                msg = Mensagens.MI006;
            }
            return retorno;
        }

        #region LOGIN

        protected void RecuperarUsuario(Pessoa pessoa, Usuario usuario, int perfilAcesso)
        {
            usuario.Login = DefinirLoginUsuario(pessoa);
            usuario.Senha = DefinirSenhaUsuario(pessoa);
            usuario.PerfilAcesso = perfilAcesso;
        }

        private string DefinirSenhaUsuario(Pessoa pessoa)
        {
            return string.Concat(pessoa.Nome.RetornaNomeSemEspacos(), pessoa.DataNascimento.Value.Year);
        }

        private string DefinirLoginUsuario(Pessoa pessoa)
        {
            return string.Concat(pessoa.Nome.RetornaNomeSemEspacos(), pessoa.Cpf.RetornaUltimosCaracteresCPF());
        }

        protected string RecuperarLoginSistema(Pessoa pessoa)
        {
            var login = "<br/><strong>Login de Acesso : </strong>" + DefinirLoginUsuario(pessoa);
            login += "<br/><strong>Senha : </strong>" + DefinirSenhaUsuario(pessoa);
            return login.ToLower();
        }

        public static int RecuperarPerfilLogado()
        {
            return perfilLogado;
        }

        public static string RecuperarNomeUsuarioLogado()
        {
            return NomeLogado;
        }

        public static string RecuperaDescrucaoPerfilLogado()
        {
            var perfil = string.Empty;
            switch (perfilLogado)
            {
                case (int)PerfilAcessoEnum.Administrador:
                    perfil = "Administrador";
                    break;
                case (int)PerfilAcessoEnum.Discente:
                    perfil = "Discente";
                    break;
                case (int)PerfilAcessoEnum.Docente:
                    perfil = "Docente";
                    break;
            }
            return perfil;
        }

        #endregion

        #region CONVERTER PARA LISTA

        /// <summary>
        /// Converte uma lista do tipo T em uma lista de ListItem pra carregar uma combo.
        /// </summary>
        /// <typeparam name="T">Tipo da lista a ser convertida.</typeparam>
        /// <param name="listaEntidade">Lista a ser convertida</param>
        /// <param name="campoTexto">Nome da propriedade a ser usada como text da combo</param>
        /// /// <param name="campoValor">Nome da propriedade a ser usada como value da combo</param>
        /// <param name="campoValor">Nome da propriedade a ser usada como value da combo</param>
        /// <param name="selecionado">Value para vir selecionado.</param>
        /// <returns>Lista convertida em ListItem</returns>
        public List<SelectListItem> ConverteListItem<T>(List<T> listaEntidade, string campoTexto, string campoValor, bool selecione = true, string selecionado = "0")
        {
            var itens = new List<SelectListItem>();

            if (selecione)
                itens.Add(new SelectListItem { Text = Constantes.SELECIONE, Value = "" });

            Type tipo = typeof(T);
            string[] propriedades = campoTexto.Split('.');

            if (listaEntidade != null)
            {
                foreach (var item in listaEntidade)
                {
                    var propriedadeText = tipo.GetProperty(campoTexto);
                    var texto = String.Empty;

                    if (propriedades.Length > 1)
                    {
                        Type tipoPropriedade = tipo.GetProperty(propriedades[0]).GetValue(item).GetType();
                        propriedadeText = tipoPropriedade.GetProperty(propriedades[1]);
                        texto = propriedadeText.GetValue(item.GetType().GetProperty(propriedades[0]).GetValue(item)).ToString();
                    }
                    else
                        texto = propriedadeText.GetValue(item).ToString();

                    itens.Add(new SelectListItem
                    {
                        Value = tipo.GetProperty(campoValor).GetValue(item).ToString(),
                        Text = texto,
                        Selected = selecionado == tipo.GetProperty(campoValor).GetValue(item).ToString()
                    });
                }
            }

            return itens;
        }

        /// <summary>
        /// Transforma Enum em uma SelectListItem. 
        /// </summary>
        /// <typeparam name="T">Enum a ser convertido.</typeparam>
        /// <param name="valorSelecionado"></param>
        /// <param name="selecione">Parametro que indica se haverá adição do item Selecione da Dropdown</param>
        /// <returns>Retorna uma SelectListItem</returns>
        public List<SelectListItem> ConverteEnumParaListItem<T>(string valorSelecionado, bool selecione)
        {
            List<SelectListItem> itens = new List<SelectListItem>();
            if (selecione)
            {
                itens = new List<SelectListItem>() { new SelectListItem { Text = Constantes.SELECIONE, Value = "" } };
            }
            var valoresEnum = Enum.GetValues(typeof(T));

            foreach (var valor in valoresEnum)
            {
                var valorConvertido = ((int)valor).ToString();
                itens.Add(new SelectListItem { Text = ((Enum)valor).GetEnumDescription(), Value = valorConvertido, Selected = valorConvertido == valorSelecionado });
            }
            return itens;
        }

        #endregion

        #region JSON

        public JsonResult RetornaJson<T>(List<T> lista, int totalRegistros)
        {
            return Json(new
            {
                sEcho = Request.Params["sEcho"].ToString(),
                iTotalRecords = lista.Count(),
                iTotalDisplayRecords = totalRegistros,
                ValidateRequest = false,
                aaData = lista
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
