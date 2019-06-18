using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Votica.Data.Infrastructure
{

    /// <summary>
    /// Repository base interface
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IRepositoryBase<T> where T : class
    {

        Task<T> AddAsync(T entity);
        Task<T> FindByAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindWhere(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(object key);
        IQueryable<T> GetAll();
        Task<int> RemoveAsync(T entity, object key);
        Task<T> UpdateAsync(T entity, object key);

    }

}
