using System.Linq.Expressions;
using EasyClinic.AppointmentsService.Domain.Entities;
using EasyClinic.AppointmentsService.Domain.Helpers;

namespace EasyClinic.AppointmentsService.Domain.Contracts
{
    /// <summary>
    /// Repository interface for <see cref="Appointment"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAppointmentResultsRepository : IRepository<AppointmentResult>
    {
        public new Task<AppointmentResult?> GetByIdAsync(Guid id);

        public new Task<List<AppointmentResult>> GetAllAsync();

        public new Task<List<AppointmentResult>> GetAllAsync(Expression<Func<AppointmentResult, bool>> predicate);

        public new Task<PagedList<AppointmentResult>> GetAllPagedAsync(Expression<Func<AppointmentResult, bool>> predicate, int pageNumber = 1, int pageSize = 20);

    }
}
