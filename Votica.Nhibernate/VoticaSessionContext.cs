using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Votica.Database.Generics;

namespace Votica.Nhibernate
{
    public class VoticaSessionContext : IDatabaseContext
    {
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public Task<int> CommitAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByAsync<T>(Expression<Func<T, bool>> match) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindWhere<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync<T>(object key) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> InsertAsync<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveAsync<T>(T entity, object key) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync<T>(T entity, object key) where T : class
        {
            throw new NotImplementedException();
        }
    }
}