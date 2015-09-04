using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Entidades;
using NHibernate;
using NHibernate.Linq;

namespace Data
{
    public interface INHibernateRepository<TEntity> where TEntity : BaseEntity
    {
        void Remove(int id);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        TEntity SaveAndReturn(TEntity entity);
        IEnumerable<TEntity> SelectWithFilter(Expression<Func<TEntity, bool>> filterCondition);
    }

    public abstract class NHibernateRepository<T> : INHibernateRepository<T>
       where T : BaseEntity
    {
        protected ISession Session { get; set; }

        protected NHibernateRepository(ISession session)
        {
            Session = session;
        }
        
        public virtual T GetById(int id)
        {
            return Session.Get<T>(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public virtual IEnumerable<T> SelectWithFilter(Expression<Func<T, bool>> filterCondition)
        {
            return Session.Query<T>().Where(filterCondition);
        }

        public T SaveAndReturn(T entity)
        {
            var entityToSave = entity;

            if (!IsUpdatingEntity(entity)) return Session.Get<T>((int) Session.Save(entityToSave));

            entityToSave = Session.Merge<T>(entity);
            Session.SaveOrUpdate(entityToSave);

            return entityToSave;
        }
        
        public virtual void Remove(int id)
        {
            var entity = GetById(id);
            Session.Delete(entity);
        }

        private static bool IsUpdatingEntity(T entidade)
        {
            return entidade.Id > 0;
        }
    }
}