using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSTest
{
    public class BaseMapper<TDtoObject, TDomain> : IDataMapper<TDomain> where TDomain : DomainBase where TDtoObject : DataEntity
    {
        private readonly IMapper _mapper;
        private readonly IGameContext _context;
        private readonly DbSet<TDtoObject> _set;

        public BaseMapper(IMapper mapper, IGameContext context)
        {
            _mapper = mapper;
            _context = context;
            _set = _context.Set<TDtoObject>();


        }
        public TDomain Add(TDomain SaveObject)
        {
            var dtoObject = _mapper.Map<TDtoObject>(SaveObject);
            _set.Add(dtoObject);
            _context.SaveChanges();
            SaveObject.Id = dtoObject.Id;
            return SaveObject;
        }

        public TDomain FetchObjectByExpress(Expression<Func<TDomain, bool>> predicate, IEnumerable<string> includes)
        {
            Expression<Func<TDtoObject, bool>> dtoPredicate = _mapper.MapExpression<Expression<Func<TDtoObject, bool>>>(predicate);
            var query = Find(dtoPredicate);
            var dtoObject = query.FirstOrDefault();
            return _mapper.Map<TDomain>(dtoObject);
        }

        public IEnumerable<TDomain> FetchObjectsByExpress(System.Linq.Expressions.Expression<Func<TDomain, bool>> predicate, IEnumerable<string> includes)
        {
            Expression<Func<TDtoObject, bool>> dtoPredicate = _mapper.MapExpression<Expression<Func<TDtoObject, bool>>>(predicate);
            var query = Find(dtoPredicate);
            return _mapper.Map<IEnumerable<TDomain>>(query);
        }



        protected IQueryable<TDtoObject> Find(Expression<Func<TDtoObject, bool>> where)
        {
            return _set.Where(where);
        }


        public TDomain Update(TDomain SaveObject)
        {
            var dtoObject = _mapper.Map<TDtoObject>(SaveObject);
            _set.Update(dtoObject);
            _context.SaveChanges();
            return SaveObject;
        }


    }
}
