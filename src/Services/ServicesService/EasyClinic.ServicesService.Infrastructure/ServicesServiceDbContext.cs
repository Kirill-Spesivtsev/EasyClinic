using EasyClinic.ServicesService.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ServicesService.Infrastructure;

/// <summary>
/// Database context for ServicesService DB.
/// </summary>
public class ServicesServiceDbContext : DbContext
{
    public ServicesServiceDbContext(DbContextOptions<ServicesServiceDbContext> options) 
    : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Service>().HasKey(d => d.Id);
        modelBuilder.Entity<ServiceCategory>().HasKey(d => d.Id);
        modelBuilder.Entity<Specialization>().HasKey(d => d.Id);

        modelBuilder.Entity<Service>()
            .HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId);

        modelBuilder.Entity<Service>()
            .HasOne(x => x.Specialization)
            .WithMany()
            .HasForeignKey(x => x.SpecializationId);

        modelBuilder.Entity<Service>()
            .Property(s => s.Name)
                .HasMaxLength(200);

        modelBuilder.Entity<Specialization>()
            .Property(s => s.Name)
                .HasMaxLength(200);


        modelBuilder.Entity<Service>()
            .Property(s => s.Price)
                .HasColumnType("decimal(19, 5)"); 

        modelBuilder.Entity<Service>()
            .HasIndex(dp => dp.Name);
    }

    public DbSet<Service> Services { get; set; }

    public DbSet<ServiceCategory> ServiceCategories { get; set; }

    public DbSet<ServiceCategory> Specializations { get; set; }
}
