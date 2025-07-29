using Hotel.Core.Entities;
using System.Linq.Expressions;

namespace Hotel.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetByIdWithTracking(int id);

        Task<T> GetByIdAsync(int id);
        IQueryable<T> Get(Expression<Func<T, bool>> expression);  

        Task AddAsync(T entity);
        Task<bool> UpdatePartialAsync(T entity, params string[] modifiedProperties);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsyncWithTracking(int id);
<<<<<<< HEAD:Hotel.Services/Interfaces/IRepository.cs
=======
        Task UpdateAsync(T entity);
>>>>>>> d6d277f (add new feature):HotelArchitecture.Core/interfaces/IRepository.cs

        Task SaveAsync();
    }
}
