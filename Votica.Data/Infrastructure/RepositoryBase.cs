using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Votica.Database.Generics;

namespace Votica.Data.Infrastructure
{
    /// <summary>
    /// RepositoryBase class with every basic method that can be called by a service
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        #region Members

        protected IDatabaseContext _ctx;

        #endregion

        #region Ctor

        public RepositoryBase(IDatabaseContext ctx)
        {
            _ctx = ctx;
        }

        #endregion

        #region Query Methods

        public async Task<T> FindByAsync(Expression<Func<T, bool>> match)
        {
            return await _ctx.FindByAsync(match);
        }

        public IQueryable<T> FindWhere(Expression<Func<T, bool>> predicate)
        {
            return _ctx.FindWhere(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _ctx.GetAll<T>();
        }

        public async Task<T> GetByIdAsync(object key)
        {
            return await _ctx.GetByIdAsync<T>(key);
        }

        #endregion

        #region Command Methods

        public async Task<T> AddAsync(T entity)
        {
            return await _ctx.InsertAsync(entity);
        }

        public async Task<int> RemoveAsync(T entity, object key)
        {
            return await _ctx.RemoveAsync(entity, key);
        }

        public async Task<T> UpdateAsync(T entity, object key)
        {
            return await _ctx.UpdateAsync(entity, key);
        }

        #endregion

    }

}
