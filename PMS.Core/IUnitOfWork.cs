using PMS.Core.Repositories.Repositories;

namespace PMS.Core.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GenericRepository<T>() where T : class;

        void Save();

        Task SaveAsync();
    }
}
