using Microsoft.EntityFrameworkCore;
using EasyClinic.AppointmentsService.Domain.Contracts;
using System.Data;
using System.Linq.Expressions;
using EasyClinic.AppointmentsService.Infrastructure;
using EasyClinic.AppointmentsService.Domain.Entities;

namespace EasyClinic.AppointmentsService.Infrastructure.Repositories
{
    /// <summary>
    /// Implements <see cref="IRepository{Appointment}"/>.
    /// Represents a generic repository.
    /// </summary>
    /// <typeparam name="Appointment"></typeparam>
    public class AppointmentsRepository : Repository<Appointment>, IAppointmentsRepository
    {
        public AppointmentsRepository(AppointmentsServiceDbContext context) 
            : base(context){}

        /// <summary>
        /// Gets an entity by id from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _context.Set<Appointment>().FindAsync(id);
        }

        /// <summary>
        /// Gets all entities from database.
        /// </summary>
        /// <returns></returns>
        public new async Task<List<Appointment>> GetAllAsync()
        {
            return await _context.Set<Appointment>().AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Gets filtered entities from database.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public new async Task<List<Appointment>> GetAllAsync(Expression<Func<Appointment, bool>>? predicate)
        {
            var query = await  _context.Set<Appointment>()
                .Where(predicate == null ? x => true : predicate)
                .OrderByDescending(x => x.Date)
                    .ThenBy(x => x.Time)
                    .ThenBy(x => x.DoctorFullName)
                    .ThenBy(x => x.ServiceName)
                .AsNoTracking()
                .ToListAsync();

            return query;
        }

    }
}
