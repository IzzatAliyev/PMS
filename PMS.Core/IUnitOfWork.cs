using PMS.Core.Repositories.Repositories;
using PMS.Infrastructure.Entities;

namespace PMS.Core.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;

        void Save();

        Task SaveAsync();
    }
}
