using Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Linq;
using Data;

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
        protected readonly INHibernateRepository<TEntidade> InHibernateRepository;

        internal readonly Regex ValidEmail =
            new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

        internal readonly Regex SingleLineCpf = new Regex(@"^\d{11}$", RegexOptions.Singleline);

        internal readonly string[] InvalidCpf = {
            "00000000000", "11111111111", "22222222222", "33333333333","44444444444",
            "55555555555", "66666666666", "77777777777","88888888888", "99999999999"
        };

        protected BaseBusiness(INHibernateRepository<TEntidade> inHibernateRepository)
        {
            InHibernateRepository = inHibernateRepository;
        }

        public virtual TEntidade GetById(int id)
        {
            return InHibernateRepository.GetById(id);
        }

        public virtual IEnumerable<TEntidade> GetAll()
        {
            return InHibernateRepository.GetAll();
        }

        public virtual IEnumerable<TEntidade> SelectWithFilter(Expression<Func<TEntidade, bool>> filterCondition)
        {
            return InHibernateRepository.SelectWithFilter(filterCondition);
        }

        public virtual void Remove(int id)
        {
            InHibernateRepository.Remove(id);
        }

        public virtual TEntidade SaveAndReturn(TEntidade entidade)
        {
            return InHibernateRepository.SaveAndReturn(entidade);
        }

        protected bool IsValidEmail(string email)
        {
            return string.IsNullOrEmpty(email) || ValidEmail.IsMatch(email);
        }

        private bool IsDefaultInvalidCpf(string cpf)
        {
            return string.IsNullOrEmpty(cpf) || InvalidCpf.Contains(cpf) || SingleLineCpf.IsMatch(cpf);
        }

        protected bool IsValidCpf(string cpf)
        {
            if (IsDefaultInvalidCpf(cpf)) return false;

            var fmtCpf = cpf;

            var total = 0;

            total += (int.Parse(fmtCpf.Substring(0, 1)) * 10);
            total += (int.Parse(fmtCpf.Substring(1, 1)) * 9);
            total += (int.Parse(fmtCpf.Substring(2, 1)) * 8);
            total += (int.Parse(fmtCpf.Substring(3, 1)) * 7);
            total += (int.Parse(fmtCpf.Substring(4, 1)) * 6);
            total += (int.Parse(fmtCpf.Substring(5, 1)) * 5);
            total += (int.Parse(fmtCpf.Substring(6, 1)) * 4);
            total += (int.Parse(fmtCpf.Substring(7, 1)) * 3);
            total += (int.Parse(fmtCpf.Substring(8, 1)) * 2);

            var digitoVerificador = total % 11;

            if (digitoVerificador < 2)
                digitoVerificador = 0;
            else
                digitoVerificador = 11 - digitoVerificador;

            if (int.Parse(fmtCpf.Substring(9, 1)) != digitoVerificador)
                return false;

            total = 0;
            total += (int.Parse(fmtCpf.Substring(0, 1)) * 11);
            total += (int.Parse(fmtCpf.Substring(1, 1)) * 10);
            total += (int.Parse(fmtCpf.Substring(2, 1)) * 9);
            total += (int.Parse(fmtCpf.Substring(3, 1)) * 8);
            total += (int.Parse(fmtCpf.Substring(4, 1)) * 7);
            total += (int.Parse(fmtCpf.Substring(5, 1)) * 6);
            total += (int.Parse(fmtCpf.Substring(6, 1)) * 5);
            total += (int.Parse(fmtCpf.Substring(7, 1)) * 4);
            total += (int.Parse(fmtCpf.Substring(8, 1)) * 3);
            total += (int.Parse(fmtCpf.Substring(9, 1)) * 2);

            digitoVerificador = total % 11;

            if (digitoVerificador < 2)
                digitoVerificador = 0;
            else
                digitoVerificador = 11 - digitoVerificador;

            return int.Parse(fmtCpf.Substring(10, 1)) == digitoVerificador;
        }
    }
}
