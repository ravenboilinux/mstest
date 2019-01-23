using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSTest
{
    public class BaseService<TDomain> : IDomainService<TDomain> where TDomain : DomainBase
    {
        private readonly IDataMapper<TDomain> _mapper;

        public BaseService(IDataMapper<TDomain> mapper)
        {
            _mapper = mapper;
        }
        public TDomain Find(Expression<Func<TDomain, bool>> expression, IEnumerable<string> includes)
        {
            return _mapper.FetchObjectByExpress(expression, includes);
        }

        public IEnumerable<TDomain> FindAll(Expression<Func<TDomain, bool>> expression, IEnumerable<string> includes)
        {
            return _mapper.FetchObjectsByExpress(expression, includes);
        }

        public TDomain GetObjectById(int id)
        {
            return _mapper.FetchObjectByExpress(u => u.Id == id, null);
        }

        public TDomain Save(TDomain domain)
        {
            if (domain.IsNew)
                return _mapper.Add(domain);
            return _mapper.Update(domain);
        }
    }
}
