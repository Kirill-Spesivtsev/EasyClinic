using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace EasyClinic.ProfilesService.Infrastructure.Repository
{
    /// <summary>
    /// Implements repository for <see cref="DoctorProfile"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoctorProfilesRepository : Repository<DoctorProfile>, IDoctorProfilesRepository
    {
        protected new readonly ProfilesServiceDbContext _context;

        public DoctorProfilesRepository(ProfilesServiceDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets an entity by id from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new async Task<DoctorProfile?> GetByIdAsync(Guid id)
        {
            return await _context.DoctorProfiles
                .Include(x => x.MedicalSpecialization)
                .Include(x => x.EmployeeStatus)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Gets all entities from database.
        /// </summary>
        /// <returns></returns>
        public new async Task<List<DoctorProfile>> GetAllAsync()
        {
            return await _context.DoctorProfiles
                .Include(x => x.MedicalSpecialization)
                .Include(x => x.EmployeeStatus)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Gets filtered entities from database.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public new async Task<List<DoctorProfile>> GetFilteredAsync(Expression<Func<DoctorProfile, bool>> predicate)
        {
            return await _context.DoctorProfiles
                .Include(x => x.MedicalSpecialization)
                .Include(x => x.EmployeeStatus)
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
