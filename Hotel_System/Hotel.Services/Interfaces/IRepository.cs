using Hotel.Core.Entities;

namespace Hotel.Services.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);
        Task<bool> UpdatePartialAsync(T entity, params string[] modifiedProperties);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsyncWithTracking(int id);

        Task SaveAsync();
    }
}
