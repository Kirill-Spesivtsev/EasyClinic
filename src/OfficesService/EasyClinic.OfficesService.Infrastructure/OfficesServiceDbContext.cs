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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Office>().HasKey(o => o.Id);
            modelBuilder.Entity<Office>().Property(o => o.City).IsRequired();
            modelBuilder.Entity<Office>().Property(o => o.Street).IsRequired();
            modelBuilder.Entity<Office>().Property(o => o.HouseNumber).IsRequired();
            modelBuilder.Entity<Office>().Property(o => o.RegistryPhone).IsRequired();
            modelBuilder.Entity<Office>().Property(o => o.Status).IsRequired();
        }

        public DbSet<Office> Offices { get; set; }

    }
}
