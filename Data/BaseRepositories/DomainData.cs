using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;
using NHibernate;
using NHibernate.Linq;

namespace Data.BaseRepositories
{
    public interface IDomainData<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> SelectWithFilter(Func<TEntity, bool> filterCondition);
    }

    public class DomainData<TEntity> : IDomainData<TEntity> where TEntity : BaseEntity
    {
        protected ISession Session { get; set; }
        protected DomainData(ISession session)
        {
            Session = session;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Session.Query<TEntity>();
        }

        public IEnumerable<TEntity> SelectWithFilter(Func<TEntity, bool> filterCondition)
        {
            return Session.Query<TEntity>().Where(filterCondition);
        }
    }
}
