using Hotel.Core.Entities;
using Hotel.Services.Interfaces;
using Hotel.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Presistance
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly HotelDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(HotelDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.Where(x => !x.IsDeleted);
        }

       

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x=>x.Id==id && !x.IsDeleted);
        }
        public async Task<T> GetByIdAsyncWithTracking(int id)
        {
            return await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePartialAsync(T entity, params string[] modifiedParams)
        {
            var existing = await _dbSet.FindAsync(entity.Id);
            if (existing == null)
                return false;

            var entry = _context.Entry(existing);

            foreach (var prop in modifiedParams)
            {
                var newValue = entity.GetType().GetProperty(prop)?.GetValue(entity);
                entry.Property(prop).CurrentValue = newValue;
                entry.Property(prop).IsModified = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
