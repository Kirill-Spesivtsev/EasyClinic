using EasyClinic.AppointmentsService.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyClinic.AppointmentsService.Domain.Contracts
{
    /// <summary>
    /// Generic Repository interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns></returns>
        public Task<List<T>> GetAllAsync();

        /// <summary>
        /// Get filtered entities.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get filtered and paged entities.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<PagedList<T>> GetAllPagedAsync(Expression<Func<T, bool>> predicate, int pageNumber = 1, int pageSize = 20);
        
        /// <summary>
        /// Creates new entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add(T entity);

        /// <summary>
        /// Updates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Update(T entity);

        /// <summary>
        /// Deletes entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Delete(T entity);

        /// <summary>
        /// Saves changes in database.
        /// </summary>
        /// <returns></returns>
        public Task SaveChangesAsync();
    }
}
