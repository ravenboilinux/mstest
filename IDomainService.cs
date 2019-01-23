
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MSTest
{
    public interface IDomainService<TDomain> where TDomain : DomainBase
    {

        TDomain GetObjectById(int id);
        TDomain Find(Expression<Func<TDomain, bool>> expression, IEnumerable<string> includes);
        IEnumerable<TDomain> FindAll(Expression<Func<TDomain, bool>> expression, IEnumerable<string> includes);
        TDomain Save(TDomain domain);
    }
}