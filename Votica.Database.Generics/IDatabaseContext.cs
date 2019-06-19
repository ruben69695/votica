using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Votica.Database.Generics
{
    /// <summary>
    /// Database Context Interface
    /// </summary>
    public interface IDatabaseContext : ICommitable
    {
        /// <summary>
        /// Insert item in the database
        /// </summary>
        /// <typeparam name="T">Entity type to insert</typeparam>
        Task<T> InsertAsync<T>(T entity) where T : class;
        Task<T> FindByAsync<T>(Expression<Func<T, bool>> match) where T : class;
        IQueryable<T> FindWhere<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> GetByIdAsync<T>(object key) where T : class;
        IQueryable<T> GetAll<T>() where T : class;
        Task<int> RemoveAsync<T>(T entity, object key) where T : class;
        Task<T> UpdateAsync<T>(T entity, object key) where T : class;
    }
}