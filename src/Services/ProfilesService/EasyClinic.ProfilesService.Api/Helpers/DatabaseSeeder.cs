using EasyClinic.ProfilesService.Domain.Entities;
using EasyClinic.ProfilesService.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EasyClinic.ProfilesService.Api.Helpers
{
    /// <summary>
    /// Helper class to automatically apply migrations 
    /// and seed initial table data on application startup.
    /// </summary>
    public static class DatabaseSeeder
    {
        /// <summary>
        /// Migrates pending schema changes to database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public static async Task SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProfilesServiceDbContext>();
            await context.Database.MigrateAsync();
            await SeedTables(context);
        }

        /// <summary>
        /// Seeds EmployeeStatuses and MedicalSpecializations table rows.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        private static async Task SeedTables(ProfilesServiceDbContext context)
        {
            string[] employeeStatuses = { "At work", "On vacation", "Sick Day", "Sick Leave", "Self-isolation", "Leave without pay", "Inactive" };
            var existingEmployeeStatuses = await context.EmployeeStatuses.Select(r => r.Name).ToListAsync();
            
            foreach (var name in employeeStatuses.Except(existingEmployeeStatuses))
            {
                context.EmployeeStatuses.Add(new EmployeeStatus{ Name = name });
            }

            string[] medicalSpecializations = { "Therapist" };
            var existingmedicalSpecializations = await context.MedicalSpecializations.Select(r => r.Name).ToListAsync();
            
            foreach (var name in medicalSpecializations.Except(existingmedicalSpecializations))
            {
                context.MedicalSpecializations.Add(new MedicalSpecialization{ Name = name });
            }

            await context.SaveChangesAsync();
        }
    }
}
