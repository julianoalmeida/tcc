using System;
using Data.BaseRepositories;
using Entidades;

namespace Negocio.BaseTypes
{
    public interface IBaseBusiness<TEntity>
     where TEntity : BaseEntity
    {
        void Remove(int id);
        TEntity GetById(int id);
        FilterResult<TEntity> GetAll();
        TEntity SaveAndReturn(TEntity entity);
        bool IsDuplicated(Func<TEntity, bool> duplicatedCondition);
        FilterResult<TEntity> SelectWithFilter(Func<TEntity, bool> filterCondition);
        FilterResult<TEntity> SelectWithPagination(Func<TEntity, bool> filterCondition, int startPage);

        void Validate(TEntity entity);
    }

    public abstract class BaseBusiness<TEntidade> : IBaseBusiness<TEntidade>
         where TEntidade : BaseEntity
    {
        private readonly IBaseRepositoryRepository<TEntidade> _repository;
        protected BaseBusiness(IBaseRepositoryRepository<TEntidade> repository)
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

        public bool IsDuplicated(Func<TEntidade, bool> duplicatedCondition)
        {
            return _repository.IsDuplicated(duplicatedCondition);
        }

        public virtual TEntidade GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual FilterResult<TEntidade> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual FilterResult<TEntidade> SelectWithFilter(Func<TEntidade, bool> filterCondition)
        {
            return _repository.SelectWithFilter(filterCondition);
        }

        public FilterResult<TEntidade> SelectWithPagination(Func<TEntidade, bool> filterCondition, int startPage)
        {
            return _repository.SelectWithPagination(filterCondition, startPage);
        }

    }
}
