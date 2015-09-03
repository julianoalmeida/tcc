using Business;
using Business.Servico;
using Comum;
using Comum.Contratos;
using Comum.Contratos.Turmas;
using Data;
using Data.Repositorio;
using Ninject.Modules;

namespace ResolvedorDependencia
{
    public class ModuloDependenciaWebService : NinjectModule
    {
        public override void Load()
        {
            #region CIDADE
            Bind<ICidadeData>().To<CidadeData>();
            Bind<ICidadeBusiness>().To<CidadeBusiness>();
            #endregion

            #region DISCIPLINA
            Bind<IDisciplinaData>().To<DisciplinaData>();
            Bind<IDisciplinaBusiness>().To<DisciplinaBusiness>();
            #endregion

            #region DOCENTE
            Bind<IDocenteData>().To<DocenteData>();
            Bind<IDocenteBusiness>().To<DocenteBusiness>();
            #endregion

            #region ADMINISTRADOR
            Bind<IAdministradorData>().To<AdministradorData>();
            Bind<IAdministradorBusiness>().To<AdministradorBusiness>();
            #endregion

            #region DISCENTE
            Bind<IDiscenteData>().To<DiscenteData>();
            Bind<IDiscenteBusiness>().To<DiscenteBusiness>();
            #endregion

            #region DOCENTE_DISCIPLINA
            Bind<IDocenteDisciplinaData>().To<DocenteDisciplinaData>();
            Bind<IDocenteDisciplinaBusiness>().To<DocenteDisciplinaBusiness>();
            #endregion

            #region ENDERECO
            Bind<IEnderecoData>().To<EnderecoData>();
            Bind<IEnderecoBusiness>().To<EnderecoBusiness>();
            #endregion

            #region ESTADO
            Bind<IEstadoData>().To<EstadoData>();
            Bind<IEstadoBusiness>().To<EstadoBusiness>();
            #endregion

            #region PESSOA
            Bind<IPessoaData>().To<PessoaData>();
            Bind<IPessoaBusiness>().To<PessoaBusiness>();
            #endregion

            #region TURMA
            Bind<ITurmaData>().To<TurmaData>();
            Bind<ITurmaBusiness>().To<TurmaBusiness>();
            #endregion

            #region USUARIO
            Bind<IUsuarioData>().To<UsuarioData>();
            Bind<IUsuarioBusiness>().To<UsuarioBusiness>();
            #endregion
        }
    }
}
