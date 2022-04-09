using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly Shopbridge_Context dbcontext;

        public Repository(Shopbridge_Context _dbcontext)
        {
            this.dbcontext = _dbcontext;
        }

        public IQueryable<T> AsQueryable<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Get<T>(params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties) where T : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
