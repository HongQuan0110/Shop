using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Mark an entity as new
        void Add(T entity);

        // Mark an entity as mofidied
        void Update(T entity);

        // Mark an entity as remove
        void Delete(T entity);

        // Delete multi records
        void DeleteMulti(Expression<Func<T, bool>> expression);

        // Get an entity by id
        T GetSingleById(int id);

        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IQueryable<T> GetAll(string[] includes = null);

        IQueryable<T> GetMulti(Expression<Func<T, bool>> expression, string[] includes = null);

        IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> expression, out int total, int index = 0, int size = 50, string[] includes = null);

        int Count(Expression<Func<T, bool>> expression);

        bool CheckContains(Expression<Func<T, bool>> expression);

    }
}
