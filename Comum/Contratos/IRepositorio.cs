using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Comum.Contratos
{
    public interface IRepositorio<T> where T : Entidade
    {
        T RemoverProxy(T model);

        IQueryable<T> Listar();

        IQueryable<T> Listar(params Expression<Func<T, object>>[] incluirPropriedades);

        T ObterPorId(int id);

        //[C]RUD
        T Criar();

        //C[R]UD
        IQueryable<T> Procurar(System.Linq.Expressions.Expression<Func<T, bool>> predicate, int limit, int offset, params Expression<Func<T, object>>[] incluirPropriedades);

        //C[R]UD
        IQueryable<T> Procurar(System.Linq.Expressions.Expression<Func<T, bool>> predicate, int limit, params Expression<Func<T, object>>[] incluirPropriedades);

        //C[R]UD
        IQueryable<T> Procurar(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] incluirPropriedades);

        //C[R]UD
        IQueryable<T> Procurar(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        //CR[U]D
        void Salvar(T entidade);

        //CR[U]D
        int SalvarComRetorno(T entidade);

        //CRU[D]
        void Apagar(T entidade);
    }
}
