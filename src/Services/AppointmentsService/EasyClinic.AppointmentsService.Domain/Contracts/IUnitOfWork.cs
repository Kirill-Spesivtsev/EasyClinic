using EasyClinic.AppointmentsService.Domain.Contracts;
using EasyClinic.AppointmentsService.Domain.Entities;

namespace EasyClinic.AppointmentsService.Domain.Contracts;

public interface IUnitOfWork
{
    public Task SaveChangesAsync();

    public IAppointmentsRepository Appointments { get; }

    public IAppointmentResultsRepository AppointmentResults { get; }
}
