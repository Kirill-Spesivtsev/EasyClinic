using EasyClinic.ProfilesService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;

namespace EasyClinic.ProfilesService.Infrastructure
{
    /// <summary>
    /// Database context for ProfilesService DB.
    /// </summary>
    public class ProfilesServiceDbContext : DbContext
    {
        public ProfilesServiceDbContext(DbContextOptions<ProfilesServiceDbContext> options) 
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DoctorProfile>().HasKey(d => d.Id);
            modelBuilder.Entity<PatientProfile>().HasKey(p => p.Id);
            modelBuilder.Entity<ReceptionistProfile>().HasKey(r => r.Id);

            modelBuilder.Entity<DoctorProfile>()
                .HasOne(d => d.MedicalSpecialization)
                .WithMany()
                .HasForeignKey(d => d.MedicalSpecializationId);

            modelBuilder.Entity<DoctorProfile>()
                .HasOne(d => d.EmployeeStatus)
                .WithMany()
                .HasForeignKey(d => d.EmployeeStatusId);
            
            modelBuilder.Entity<PatientProfile>()
                .HasIndex(dp => dp.FirstName).IsUnique(false);
            modelBuilder.Entity<PatientProfile>()
                .HasIndex(dp => dp.LastName).IsUnique(false);
            modelBuilder.Entity<PatientProfile>()
                .HasIndex(dp => dp.FullName).IsUnique(false);

            modelBuilder.Entity<DoctorProfile>()
                .HasIndex(dp => dp.FirstName).IsUnique(false);
            modelBuilder.Entity<DoctorProfile>()
                .HasIndex(dp => dp.LastName).IsUnique(false);
            modelBuilder.Entity<DoctorProfile>()
                .HasIndex(dp => dp.FullName).IsUnique(false);

            modelBuilder.Entity<ReceptionistProfile>()
                .HasIndex(dp => dp.FirstName).IsUnique(false);
            modelBuilder.Entity<ReceptionistProfile>()
                .HasIndex(dp => dp.LastName).IsUnique(false);
            modelBuilder.Entity<ReceptionistProfile>()
                .HasIndex(dp => dp.FullName).IsUnique(false);

        }

        public DbSet<PatientProfile> PatientProfiles { get; set; }

        public DbSet<DoctorProfile> DoctorProfiles { get; set; }

        public DbSet<ReceptionistProfile> ReceptionistProfiles { get; set; }

        public DbSet<MedicalSpecialization> MedicalSpecializations { get; set; }

        public DbSet<EmployeeStatus> EmployeeStatuses { get; set; }

    }
}