using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Comum.Exceptions;
using Data;
using Negocio.RequiredFieldValidators;

namespace Negocio
{
    public interface INegocioBase<TEntity>
     where TEntity : BaseEntity
    {
        void Remove(int id);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        TEntity SaveAndReturn(TEntity entity);
        IEnumerable<TEntity> SelectWithFilter(Expression<Func<TEntity, bool>> filterCondition);
    }

    public abstract class BaseBusiness<TEntidade> : INegocioBase<TEntidade>
         where TEntidade : BaseEntity
    {
        private readonly INHibernateRepository<TEntidade> _repository;
        private readonly IEnumerable<IRequiredFieldsValidator> _requiredFieldValidator;

        protected BaseBusiness(INHibernateRepository<TEntidade> repository)
        {
            _repository = repository;
            _requiredFieldValidator = DependencyResolver.Current.GetServices<IRequiredFieldsValidator>();
        }

        public virtual TEntidade GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual IEnumerable<TEntidade> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual IEnumerable<TEntidade> SelectWithFilter(Expression<Func<TEntidade, bool>> filterCondition)
        {
            return _repository.SelectWithFilter(filterCondition);
        }

        public virtual void Remove(int id)
        {
            _repository.Remove(id);
        }

        public virtual TEntidade SaveAndReturn(TEntidade entidade)
        {
            HasRequiredFieldsNotFilled(entidade);

            return _repository.SaveAndReturn(entidade);
        }

        private void HasRequiredFieldsNotFilled(BaseEntity entity)
        {
            var validator = _requiredFieldValidator.Single(a => a.CanValidate(entity));

            if (validator == null)
                throw new MissingMemberException("Can't found any validator for this class");

            validator.Validate(entity);
        }

        protected bool IsValidEmail(string email)
        {
            var validEmail =
                new Regex(
                    @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            return string.IsNullOrEmpty(email) || validEmail.IsMatch(email);
        }
    }
}
