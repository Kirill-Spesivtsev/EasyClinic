using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Domain.Contracts
{
    /// <summary>
    /// Generic Repository interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
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
        public Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Creates new entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<T> AddAsync(T entity);

        /// <summary>
        /// Deletes entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task DeleteAsync(T entity);

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Updates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(T entity);
    }
}
