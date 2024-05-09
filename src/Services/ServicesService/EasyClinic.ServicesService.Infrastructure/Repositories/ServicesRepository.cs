using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ServicesService.Infrastructure.Repository
{
    /// <summary>
    /// Represents repository for <see cref="Service"/>.
    /// Overrides necessary methods from <see cref="Repository{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServicesRepository : Repository<Service>, IServicesRepository
    {
        protected new readonly ServicesServiceDbContext _context;

        public ServicesRepository(ServicesServiceDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets an entity by id from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _context.Services
                .Include(x => x.Category)
                .Include(x => x.Specialization)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Gets all entities from database.
        /// </summary>
        /// <returns></returns>
        public new async Task<List<Service>> GetAllAsync()
        {
            return await _context.Services
                .Include(x => x.Category)
                .Include(x => x.Specialization)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Gets filtered entities from database by the predicate expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public new async Task<List<Service>> GetFilteredAsync(Expression<Func<Service, bool>> predicate)
        {
            return await _context.Services
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
