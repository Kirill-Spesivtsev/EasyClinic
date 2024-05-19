using System.Linq.Expressions;
using EasyClinic.AppointmentsService.Domain.Entities;
using EasyClinic.AppointmentsService.Domain.Helpers;

namespace EasyClinic.AppointmentsService.Domain.Contracts
{
    /// <summary>
    /// Repository interface for <see cref="Appointment"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAppointmentsRepository : IRepository<Appointment>
    {
        public new Task<Appointment> GetByIdAsync(Guid id);

        public new Task<List<Appointment>> GetAllAsync();

        public new Task<List<Appointment>> GetAllAsync(Expression<Func<Appointment, bool>> predicate);

        public new Task<PagedList<Appointment>> GetPagedAsync(Expression<Func<Appointment, bool>> predicate, int pageNumber = 1, int pageSize = 20);

    }
}
