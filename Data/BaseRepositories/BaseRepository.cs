using System;
using System.Collections.Generic;
using System.Linq;
using Comum;
using Entidades;
using NHibernate;
using NHibernate.Linq;

namespace Data.BaseRepositories
{
    public interface IBaseRepositoryRepository<TEntity> where TEntity : BaseEntity
    {
        void Remove(int id);
        TEntity GetById(int id);
        FilterResult<TEntity> GetAll();
        TEntity SaveAndReturn(TEntity entity);
        FilterResult<TEntity> SelectWithFilter(Func<TEntity, bool> filterCondition);
        FilterResult<TEntity> SelectWithPagination(Func<TEntity, bool> filterCondition, int startPage);
    }

    public abstract class BaseRepositoryRepository<T> : IBaseRepositoryRepository<T>
       where T : BaseEntity
    {
        protected ISession Session { get; set; }
        protected BaseRepositoryRepository(ISession session)
        {
            Session = session;
        }
        
        public virtual void Remove(int id)
        {
            var entity = GetById(id);
            Session.Delete(entity);
        }

        public T SaveAndReturn(T entity)
        {
            var entityToSave = entity;

            if (!IsUpdatingEntity(entity)) return Session.Get<T>((int)Session.Save(entityToSave));

            entityToSave = Session.Merge<T>(entity);
            Session.SaveOrUpdate(entityToSave);

            return entityToSave;
        }

        public virtual T GetById(int id)
        {
            return Session.Get<T>(id);
        }

        public virtual FilterResult<T> GetAll()
        {
            var result = Session.Query<T>();
            return BuildFilterResult(result, result.Count());
        }

        public FilterResult<T> SelectWithPagination(Func<T, bool> filterCondition, int startPage)
        {
            var result = SelectWithFilter(filterCondition);
            return BuildFilterResult(Paginate(result.Values, startPage), result.Values.Count());
        }

        public virtual FilterResult<T> SelectWithFilter(Func<T, bool> filterCondition)
        {
            var result = Session.Query<T>().Where(filterCondition);
            return BuildFilterResult(result, result.Count());
        }

        private static IEnumerable<T> Paginate(IEnumerable<T> list, int startPage)
        {
            return list.Skip(startPage).Take(Constants.TOTAL_PAGE_REGISTERS);
        }

        private static bool IsUpdatingEntity(T entidade)
        {
            return entidade.Id > 0;
        }

        private static FilterResult<T> BuildFilterResult(IEnumerable<T> query, int total)
        {
            return new FilterResult<T> { Values = query, Total = total };
        }
    }
}