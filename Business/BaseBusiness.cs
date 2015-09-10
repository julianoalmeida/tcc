using Entidades;
using System;
using System.Linq.Expressions;
using Data.BaseRepositories;

namespace Negocio
{
    public interface IBaseBusiness<TEntity>
     where TEntity : BaseEntity
    {
        void Remove(int id);
        TEntity GetById(int id);
        FilterResult<TEntity> GetAll();
        TEntity SaveAndReturn(TEntity entity);
        FilterResult<TEntity> SelectWithFilter(Expression<Func<TEntity, bool>> filterCondition);
        FilterResult<TEntity> SelectWithPagination(Expression<Func<TEntity, bool>> filterCondition, int startPage);

        void Validate(TEntity entity);
    }

    public abstract class BaseBusinessBusiness<TEntidade> : IBaseBusiness<TEntidade>
         where TEntidade : BaseEntity
    {
        private readonly IBaseRepositoryRepository<TEntidade> _repository;
        protected BaseBusinessBusiness(IBaseRepositoryRepository<TEntidade> repository)
        {
            _repository = repository;
        }

        public virtual void Remove(int id)
        {
            _repository.Remove(id);
        }

        public abstract void Validate(TEntidade entity);

        public virtual TEntidade SaveAndReturn(TEntidade entidade)
        {
            Validate(entidade);
            return _repository.SaveAndReturn(entidade);
        }

        public virtual TEntidade GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual FilterResult<TEntidade> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual FilterResult<TEntidade> SelectWithFilter(Expression<Func<TEntidade, bool>> filterCondition)
        {
            return _repository.SelectWithFilter(filterCondition);
        }

        public FilterResult<TEntidade> SelectWithPagination(Expression<Func<TEntidade, bool>> filterCondition, int startPage)
        {
            return _repository.SelectWithPagination(filterCondition, startPage);
        }

    }
}
