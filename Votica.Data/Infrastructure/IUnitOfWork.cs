using System.Threading.Tasks;

namespace Votica.Data.Infrastructure
{
    public interface IUnitOfWork
    {

        IRepositoryBase<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Commit();
        Task<int> CommitAsync();

    }
}
