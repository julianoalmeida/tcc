using System;
using Castle.DynamicProxy;
using NHibernate;

namespace AutoFacConfig.Interceptadores
{   
    public class ServiceTransactionInterceptor : Castle.DynamicProxy.IInterceptor
    {
        private readonly ISession _databaseSession;
        private ITransaction _transaction;

        public ServiceTransactionInterceptor(ISession databaseSession)
        {
            _databaseSession = databaseSession;
        }
        
        public void Intercept(IInvocation invocation)
        {
            var iAmTheFirst = false;

            if (_transaction == null)
            {
                _transaction = _databaseSession.BeginTransaction();
                iAmTheFirst = true;
            }

            try
            {
                invocation.Proceed();

                if (iAmTheFirst)
                {
                    iAmTheFirst = false;

                    _transaction.Commit();
                    _transaction = null;
                }
            }
            catch (Exception)
            {
                if (iAmTheFirst)
                {
                    iAmTheFirst = false;

                    _transaction.Rollback();
                    _databaseSession.Clear();
                    _transaction = null;
                }
                throw;
            }
        }
    }
}
