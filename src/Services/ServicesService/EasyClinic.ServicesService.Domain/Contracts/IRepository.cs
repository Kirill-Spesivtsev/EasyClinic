﻿using System.Data;
using System.Linq.Expressions;

namespace EasyClinic.ServicesService.Domain.Contracts
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

        /// <summary>
        /// Returns transaction.
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns><see cref="IDbTransaction"/> instance</returns>
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}