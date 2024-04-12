using EasyClinic.ProfilesService.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Infrastructure.Repository
{
    /// <summary>
    /// Implements <see cref="IRepository{T}"/>.
    /// Represents a generic repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ProfilesServiceDbContext _context;

        public Repository(ProfilesServiceDbContext context)
        {
            _context = context;
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
        public async Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Creates new entity in database.
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
        /// Deletes an entity from database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
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
        /// Updates an entity in database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns an a transaction.
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns><see cref="IDbTransaction"/> instance</returns>
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var transaction = _context.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }
    }
}
