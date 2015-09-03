using Comum.Contratos;
using Entidades;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Proxy;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repositorio
{
    public abstract class RepositorioNHibernate<T> : IRepositorio<T>
       where T : Entidade
    {

        protected ISession Session { get; set; }

        public RepositorioNHibernate(ISession session)
        {
            this.Session = session;
        }

        public T RemoverProxy(T model)
        {
            model = RemoverProxyPropriedades<T>(model);
            return (T)Session.GetSessionImplementation().PersistenceContext.Unproxy(model);
        }

        private E RemoverProxyPropriedades<E>(E model)
        {
            var properties = model.GetType().GetProperties();

            foreach (var property in properties)
            {
                var valor = property.GetValue(model);
                if (valor.IsProxy())
                {
                    if (!NHibernateUtil.IsInitialized(valor))
                    {
                        NHibernateUtil.Initialize(valor);
                    }
                    property.SetValue(model, RemoverProxyPropriedades(valor));
                }
            }
            return (E)Session.GetSessionImplementation().PersistenceContext.Unproxy(model);
        }


        public virtual T ObterPorId(int id)
        {
            var entidade = Session.Get<T>(id);
            //Session.Evict(entidade);
            return entidade;
        }

        public virtual IQueryable<T> Procurar(Expression<Func<T, bool>> predicate)
        {
            return Session.Query<T>().Where(predicate);
        }

        public virtual IQueryable<T> Procurar(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] incluirPropriedades)
        {
            var query = Session.Query<T>().Where(predicate);

            FetchProperties(incluirPropriedades, query);

            return query;
        }

        public virtual IQueryable<T> Procurar(Expression<Func<T, bool>> predicate, int limit = 10, params Expression<Func<T, object>>[] incluirPropriedades)
        {
            var query = Session.Query<T>().Where(predicate);

            FetchProperties(incluirPropriedades, query);

            return query.Take(limit);
        }

        public virtual IQueryable<T> Procurar(Expression<Func<T, bool>> predicate, int limit = 10, int offset = 0, params Expression<Func<T, object>>[] incluirPropriedades)
        {
            var query = Session.Query<T>().Where(predicate);

            FetchProperties(incluirPropriedades, query);

            return query.Skip(offset).Take(limit);
        }


        public virtual IQueryable<T> Listar()
        {
            return Session.Query<T>();
        }

        public virtual void Salvar(T entidade)
        {
            var entidadeASalvar = entidade;

            if (EstaAtualizando(entidade))
            {
                entidadeASalvar = Session.Merge<T>(entidade);
            }

            Session.SaveOrUpdate(entidadeASalvar);

        }

        public virtual int SalvarComRetorno(T entidade)
        {
            var entidadeASalvar = entidade;

            if (EstaAtualizando(entidade))
            {
                entidadeASalvar = Session.Merge<T>(entidade);
            }

            var id = (int)Session.Save(entidadeASalvar);
            return id;
        }

        private static bool EstaAtualizando(T entidade)
        {
            return entidade.Id > 0;
        }

        public virtual void Apagar(int id)
        {
            var entidade = ObterPorId(id);
            Session.Delete(entidade);
        }

        public virtual void Apagar(T entidade)
        {
            Session.Delete(entidade);
        }

        public virtual T Criar()
        {
            return Activator.CreateInstance<T>();
        }

        public IQueryable<T> Listar(params Expression<Func<T, object>>[] incluirPropriedades)
        {
            Session.Clear();
            var query = Session.Query<T>();

            FetchProperties(incluirPropriedades, query);

            return query;
        }

        private IQueryable<T> FetchProperties(Expression<Func<T, object>>[] incluirPropriedades, IQueryable<T> query)
        {
            if (incluirPropriedades != null)
            {
                foreach (var lambdaPropriedade in incluirPropriedades)
                {
                    query = query.Fetch(lambdaPropriedade);
                }
            }

            return query;
        }
    }
}
