using System.Linq.Expressions;

namespace PMS.Core.Repositories.Repositories
{
    public interface IGenericRepository<T> : IDisposable
    {
        void Add(T entity);

        Task<T> AddAsync(T entity);

        void Update(T entity);

        Task<T> UpdateAsync(T entity);

        void Delete(T entity);

        Task<T> DeleteAsync(T entity);

        T GetById(object id);

        Task<T> GetByIdAsync(object id);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedEnumerable<T>>? orderBy = null,
            string includeProperties = "");
    }
}
