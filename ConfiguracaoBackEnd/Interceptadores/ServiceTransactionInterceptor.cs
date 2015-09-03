using System;
using Castle.DynamicProxy;
using log4net;
using NHibernate;

namespace AutoFacConfig.Interceptadores
{
    /// <summary>
    /// Interceptador responsavel por gerenciar a transação do NHibernate durante o escopo do metodo do serviço.
    /// Caso alguma exceção seja lançada da camada de negocio para frente, a transação sofre um rollback.    
    /// </summary>
    public class ServiceTransactionInterceptor : Castle.DynamicProxy.IInterceptor
    {
        private readonly ISession db;
        private readonly ILog logger;
        private ITransaction transaction = null;

        public ServiceTransactionInterceptor(ISession db, ILog log)
        {
            this.db = db;
            this.logger = log;
        }

        /// <summary>
        /// Metodo disparado toda vez que uma chamada a qualquer serviço é realizada.
        /// Antes de prosseguir com a execução do metodo, a transação de dados é iniciada.
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            bool iAmTheFirst = false;

            if (transaction == null)
            {
                transaction = db.BeginTransaction();
                iAmTheFirst = true;
            }

            try
            {
                invocation.Proceed();

                if (iAmTheFirst)
                {
                    iAmTheFirst = false;

                    transaction.Commit();
                    transaction = null;
                }
            }
            catch (Exception ex)
            {
                if (iAmTheFirst)
                {
                    iAmTheFirst = false;

                    transaction.Rollback();
                    db.Clear();
                    transaction = null;
                }
                logger.Error("O interceptador de serviço capturou uma exceção.", ex);
                throw;
            }
        }
    }
}
