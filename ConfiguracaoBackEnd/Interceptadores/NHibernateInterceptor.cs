using System.Collections;
using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Type;

namespace AutoFacConfig.Interceptadores
{
    public class NHibernateInterceptor : EmptyInterceptor
    {
        public new bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            return base.OnSave(entity, id, state, propertyNames, types);
        }

        public new void OnDelete(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            base.OnDelete(entity, id, state, propertyNames, types);
        }

        public new void PostFlush(ICollection entities)
        {
            base.PostFlush(entities);
        }

        public new SqlString OnPrepareStatement(SqlString sql)
        {
            return base.OnPrepareStatement(sql);
        }
    }
}
