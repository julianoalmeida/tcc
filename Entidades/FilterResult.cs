using System.Collections.Generic;

namespace Entidades
{
    public class FilterResult<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Values { get; set; }
        public int Total { get; set; }
    }
}
