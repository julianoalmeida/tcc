
namespace Comum
{
    public static class Constantes
    {

        #region PAGINAÇÃO

        /// <summary>
        /// Total de registro por páginas
        /// </summary>
        public const int TOTAL_REGISTRO_POR_PAGINAS = 10;
        
        /// <summary>
        /// Página atual do datatables
        /// </summary>
        public const string PAGINA_ATUAL = "iDisplayStart";


        /// <summary>
        /// Constante para identificar qual foi o tipo de ordenação selecionado. (asc ou desc)
        /// </summary>
        public const string TIPO_ORDENACAO = "sSortDir_0";

        /// <summary>
        /// iSortCol_0 - Constante para identificar qual foi o campo se ordenação selecionado.
        /// </summary>
        public const string CAMPO_ORDENACAO = "iSortCol_0";

        /// <summary>
        /// Ordenação "asc"
        /// </summary>
        public const string CRESCENTE = "asc";

        /// <summary>
        /// Ordenação "desc"
        /// </summary>
        public const string DECRESCENTE = "desc";


        #endregion

        #region AÇÕES

        /// <summary>
        /// Incluir
        /// </summary>
        public const string INCLUIR = "Incluir";

        /// <summary>
        /// Selecione
        /// </summary>
        public const string SELECIONE = "Selecione";
        
        /// <summary>
        /// Excluir
        /// </summary>
        public const string EXCLUIR = "Excluir";
        
        /// <summary>
        /// Manter.
        /// </summary>
        public const string MANTER = "Manter";

        /// <summary>
        /// Index.
        /// </summary>
        public const string INDEX = "Index";

        #endregion

        #region LOGIN
        public const string USUARIO_LOGADO = "UsuarioLogado";
        #endregion
    }
}