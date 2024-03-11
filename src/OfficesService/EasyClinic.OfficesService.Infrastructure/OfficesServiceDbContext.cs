using Microsoft.EntityFrameworkCore;
using EasyClinic.OfficesService.Domain.Entities;

namespace EasyClinic.OfficesService.Infrastructure
{
    public class OfficesServiceDbContext : DbContext
    {
        public OfficesServiceDbContext(DbContextOptions<OfficesServiceDbContext> options) 
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Office> Offices { get; set; }

    }
}
