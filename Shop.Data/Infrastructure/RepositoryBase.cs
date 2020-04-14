using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties
        private ShopDbContext ShopDbContext;
        private IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected ShopDbContext DbContext
        {
            get { return ShopDbContext ?? (ShopDbContext = DbFactory.Init()); }
        }
        #endregion

        public RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }
        
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }


        public void Update(T entity)
        {
            dbSet.Attach(entity);
            ShopDbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteMulti(Expression<Func<T, bool>> expression)
        {
            IEnumerable<T> objects = dbSet.Where(expression).AsEnumerable();
            foreach(var obj in objects)
            {
                dbSet.Remove(obj);
            }
        }

        #region Get

        public T GetSingleById(int id)
        {
            return dbSet.Find(id);
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            return GetAll(includes).FirstOrDefault(expression);
        }

        public IQueryable<T> GetAll(string[] includes = null)
        {
            // Handle includes for associated object if applicable
            if (includes != null && includes.Count() > 0)
            {
                var query = dbSet.Include(includes.First());
                foreach(var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                return query.AsQueryable();
            }
            return dbSet.AsQueryable();
        }

        public IQueryable<T> GetMulti(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            // Handle includes for associated object if applicable
            if (includes != null && includes.Count() > 0)
            {
                var query = dbSet.Include(includes.First());
                foreach(var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                return query.Where(expression).AsQueryable();
            }
            return dbSet.Where(expression);
        }

        public IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> expression, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            // Handle includes for associated object if applicable
            if(includes != null && includes.Count() > 0)
            {
                var query = dbSet.Include(includes.First());
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
                _resetSet = expression != null ? query.Where(expression).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = expression != null ? dbSet.Where(expression).AsQueryable() : dbSet.AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet;
        }

        #endregion

        public int Count(Expression<Func<T, bool>> expression)
        {
            return dbSet.Count(expression);
        }

        public bool CheckContains(Expression<Func<T, bool>> expression)
        {
            return dbSet.Count(expression) > 0;
        }
    }
}
