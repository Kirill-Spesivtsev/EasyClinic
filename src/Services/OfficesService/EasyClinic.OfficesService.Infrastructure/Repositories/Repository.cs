using EasyClinic.OfficesService.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace EasyClinic.OfficesService.Infrastructure.Repository
{
    /// <summary>
    /// Implements <see cref="IRepository{T}"/>.
    /// Represents a generic repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly OfficesServiceDbContext _context;

        public Repository(OfficesServiceDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all entities from database.
        /// </summary>
        /// <returns>Collection of entities</returns>
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Adds new entity to database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Deletes entity from database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets entity by id from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entity or null if not found</returns>
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Updates entity in database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }


    }
}
