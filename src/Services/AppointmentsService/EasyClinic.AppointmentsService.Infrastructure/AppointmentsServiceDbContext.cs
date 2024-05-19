using EasyClinic.AppointmentsService.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace EasyClinic.AppointmentsService.Infrastructure
{
    /// <summary>
    /// Database context for AppointmentsService DB.
    /// </summary>
    public class AppointmentsServiceDbContext : DbContext
    {
        public AppointmentsServiceDbContext(DbContextOptions<AppointmentsServiceDbContext> options) 
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>().HasKey(d => d.Id);
            modelBuilder.Entity<AppointmentResult>().HasKey(d => d.Id);
            modelBuilder.Entity<AppointmentResult>().HasKey(d => d.Id);

            modelBuilder.Entity<AppointmentResult>().Property(d => d.AppointmentId).IsRequired();
            modelBuilder.Entity<Document>().Property(d => d.AppointmentResultId).IsRequired();

            modelBuilder.Entity<AppointmentResult>()
                .HasOne(x => x.Appointment)
                .WithOne()
                .HasForeignKey<AppointmentResult>(x => x.AppointmentId);

            modelBuilder.Entity<Document>()
                .HasOne(x => x.AppointmentResult)
                .WithMany()
                .HasForeignKey(x => x.AppointmentResultId);
        }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<AppointmentResult> AppointmentResults { get; set; }

        public DbSet<Document> Documents { get; set; }

    }
}