using System.Threading.Tasks;

namespace Votica.Database.Generics
{
    public interface ICommitable
    {
        void Commit();
        Task<int> CommitAsync();
    }
}