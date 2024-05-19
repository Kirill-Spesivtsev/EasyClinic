using Microsoft.EntityFrameworkCore;
using EasyClinic.AppointmentsService.Domain.Contracts;
using System.Data;
using System.Linq.Expressions;
using EasyClinic.AppointmentsService.Infrastructure;
using EasyClinic.AppointmentsService.Domain.Entities;
using EasyClinic.AppointmentsService.Domain.Entities;
using EasyClinic.AppointmentsService.Domain.Helpers;

namespace EasyClinic.AppointmentsService.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for AppointmentResultResult.
    /// Implements <see cref="IRepository{AppointmentResult}"/>.
    /// </summary>
    /// <typeparam name="AppointmentResult"></typeparam>
    public class AppointmentResultsRepository : Repository<AppointmentResult>, IAppointmentResultsRepository
    {
        public AppointmentResultsRepository(AppointmentsServiceDbContext context) 
            : base(context){}

        /// <summary>
        /// Gets an entity by id from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new async Task<AppointmentResult?> GetByIdAsync(Guid id)
        {
            return await _context.Set<AppointmentResult>().FindAsync(id);
        }

        /// <summary>
        /// Gets all entities from database.
        /// </summary>
        /// <returns></returns>
        public new async Task<List<AppointmentResult>> GetAllAsync()
        {
            return await _context.Set<AppointmentResult>().AsNoTracking().ToListAsync();
        }

        
        /// <summary>
        /// Gets filtered entities from database.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public new async Task<List<AppointmentResult>> GetAllAsync(Expression<Func<AppointmentResult, bool>>? predicate)
        {
            var query = await  _context.Set<AppointmentResult>()
                .Where(predicate == null ? x => true : predicate)
                .Include(x => x.Appointment)
                .AsNoTracking()
                .ToListAsync();

            return query;
        }
    }
}
