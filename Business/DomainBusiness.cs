﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Data;
using Data.BaseRepositories;
using Entidades;

namespace Negocio
{
    public interface IDomainBusiness<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> SelectWithFilter(Expression<Func<TEntity, bool>> filterCondition);
    }

    public class DomainBusiness<TEntity> : IDomainBusiness<TEntity> where TEntity : BaseEntity
    {
        private readonly IDomainData<TEntity> _repository;
        protected DomainBusiness(IDomainData<TEntity> repository)
        {
            _repository = repository;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<TEntity> SelectWithFilter(Expression<Func<TEntity, bool>> filterCondition)
        {
            return _repository.SelectWithFilter(filterCondition);
        }
    }
}
