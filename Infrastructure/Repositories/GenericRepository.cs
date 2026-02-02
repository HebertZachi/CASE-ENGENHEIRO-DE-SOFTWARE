using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> FindById(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllBylimit(int page = 1, int limit = 10)
        {
            if (page < 1) page = 1;
            if (limit < 1) limit = 10;

            return await _dbSet
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateById(Guid id, T updatedEntity)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
            if (entity == null)
                throw new InvalidOperationException("Entity not found or has been deleted.");

            _context.Entry(entity).CurrentValues.SetValues(updatedEntity);
            entity.SetUpdated();

            await _context.SaveChangesAsync();
        }

        public async Task SoftDelete(Guid id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
            if (entity == null)
                throw new InvalidOperationException("Entity not found or already deleted.");

            entity.SetDelete();
            _dbSet.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
