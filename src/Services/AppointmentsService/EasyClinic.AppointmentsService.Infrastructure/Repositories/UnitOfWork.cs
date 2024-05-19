using EasyClinic.AppointmentsService.Domain.Contracts;


namespace EasyClinic.AppointmentsService.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppointmentsServiceDbContext _db;
    private readonly AppointmentsRepository _appointmentRepository;
    private readonly AppointmentResultsRepository _appointmentResultRepository;

    public IAppointmentsRepository Appointments
    { 
        get => _appointmentRepository;
    }

    public IAppointmentResultsRepository AppointmentResults
    {
        get => _appointmentResultRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
    public UnitOfWork(AppointmentsServiceDbContext db)
    {
        _db = db;
        _appointmentRepository = new AppointmentsRepository(_db);
        _appointmentResultRepository = new AppointmentResultsRepository(_db);
    }

    private bool disposed = false;

    public virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}