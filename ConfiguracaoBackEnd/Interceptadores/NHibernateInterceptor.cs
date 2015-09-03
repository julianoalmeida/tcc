using NHibernate;
using NHibernate.Type;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfiguracaoBackEnd.Interceptadores
{
    public class NHibernateInterceptor : EmptyInterceptor
    {
        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            return base.OnSave(entity, id, state, propertyNames, types);
        }

        public override void OnDelete(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            base.OnDelete(entity, id, state, propertyNames, types);
        }

        public override void PostFlush(ICollection entities)
        {
            base.PostFlush(entities);
        }

        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            return base.OnPrepareStatement(sql);
        }
    }
}
