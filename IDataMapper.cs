using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MSTest
{
    public interface IDataMapper<TDomain> where TDomain : DomainBase
    {
        TDomain FetchObjectByExpress(Expression<Func<TDomain, bool>> predicate, IEnumerable<string> includes);

        IEnumerable<TDomain> FetchObjectsByExpress(Expression<Func<TDomain, bool>> predicate, IEnumerable<string> includes);

        TDomain Add(TDomain SaveObject);

        TDomain Update(TDomain SaveObject);
    }
}