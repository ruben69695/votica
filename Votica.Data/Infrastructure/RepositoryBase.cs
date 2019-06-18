using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Votica.Data.Infrastructure
{

    public class VoticaContext : DbContext
    {
        // Fake class
    }

    /// <summary>
    /// RepositoryBase class with every basic method that can be called by a service
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        #region Members

        protected VoticaContext _ctx;

        #endregion

        #region Ctor

        public RepositoryBase(VoticaContext ctx)
        {
            _ctx = ctx;
        }

        #endregion

        #region Query Methods

        public async Task<T> FindByAsync(Expression<Func<T, bool>> match)
        {
            return await _ctx.Set<T>().SingleOrDefaultAsync(match);
        }

        public IQueryable<T> FindWhere(Expression<Func<T, bool>> predicate)
        {
            return _ctx.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _ctx.Set<T>();
        }

        public async Task<T> GetByIdAsync(object key)
        {
            return await _ctx.Set<T>().FindAsync(key);
        }

        #endregion

        #region Command Methods

        public async Task<T> AddAsync(T entity)
        {
            await _ctx.Set<T>().AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<int> RemoveAsync(T entity, object key)
        {
            _ctx.Set<T>().Remove(entity);
            return await _ctx.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity, object key)
        {
            if (entity == null)
                return null;

            T entityExist = await _ctx.Set<T>().FindAsync(key);
            if (entityExist != null)
            {
                _ctx.Entry(entityExist).CurrentValues.SetValues(entity);
                await _ctx.SaveChangesAsync();
            }
            return entityExist;
        }

        #endregion

    }

}
