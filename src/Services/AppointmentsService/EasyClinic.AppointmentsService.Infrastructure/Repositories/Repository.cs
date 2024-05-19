using Microsoft.EntityFrameworkCore;
using EasyClinic.AppointmentsService.Domain.Contracts;
using System.Data;
using System.Linq.Expressions;
using EasyClinic.AppointmentsService.Infrastructure;
using EasyClinic.AppointmentsService.Domain.Helpers;
using EasyClinic.AppointmentsService.Domain.Helpers;
using EasyClinic.AppointmentsService.Application.Helpers;

namespace EasyClinic.AppointmentsService.Infrastructure.Repositories
{
    /// <summary>
    /// Implements <see cref="IRepository{T}"/>.
    /// Represents a generic repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppointmentsServiceDbContext _context;

        public Repository(AppointmentsServiceDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets an entity by id from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Gets all entities from database.
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Gets filtered entities from database.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Gets filtered and paged entities from database.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<PagedList<T>> GetAllPagedAsync(Expression<Func<T, bool>> predicate, int pageNumber = 1, int pageSize = 20)
        {
            var query = _context.Set<T>()
                .Where(predicate)
                .AsNoTracking();
                
            int count = query.Count();

            return await query.GetPage(count, pageNumber, pageSize);
        }

        /// <summary>
        /// Creates new entity in database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        /// <summary>
        /// Updates an entity in database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes an entity from database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Saves changes in database.
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
