using EasyClinic.ServicesService.Domain.Entities;
using EasyClinic.ServicesService.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EasyClinic.ServicesService.Api.Helpers
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
        /// <param name="app"></param>
        /// <returns></returns>
        public static async Task SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ServicesServiceDbContext>();
            await context.Database.MigrateAsync();
            await SeedTables(context);
        }

        /// <summary>
        /// Seeds ServiceCategories table rows.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static async Task SeedTables(ServicesServiceDbContext context)
        {
            var categories = new[] 
            { 
                new { Name = "Analyzes", TimeSlotSize = 1 },
                new { Name = "Consultations", TimeSlotSize = 2 },
                new { Name = "Diagnostics", TimeSlotSize = 3 },
                new { Name = "Surgery", TimeSlotSize = 8 }
            };  

            var existing = context.ServiceCategories
                .Select(r => r.Name)
                .Intersect(categories.Select(r => r.Name))
                .AsEnumerable();

            var missing = categories.Where(c => !existing.Contains(c.Name));
            
            foreach (var category in missing)
            {
                context.ServiceCategories.Add(new ServiceCategory{ 
                    Name = category.Name,
                    TimeSlotSize = category.TimeSlotSize
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
