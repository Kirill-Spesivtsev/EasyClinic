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
        /// <typeparam name="T"></typeparam>
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
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        private static async Task SeedTables(ServicesServiceDbContext context)
        {
            string[] categories = { "Consultations", "Analyzes", "Diagnostics", "Surgery" };
            var existingCategories = await context.Services.Select(r => r.Name).ToListAsync();
            
            foreach (var name in categories.Except(existingCategories))
            {
                context.ServiceCategories.Add(new ServiceCategory{ Name = name });
            }

            await context.SaveChangesAsync();
        }
    }
}
