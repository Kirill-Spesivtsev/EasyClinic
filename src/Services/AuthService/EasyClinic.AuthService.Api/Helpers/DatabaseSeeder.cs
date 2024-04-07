using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyClinic.AuthService.Api.Helpers
{
    /// <summary>
    /// Helper class to automatically apply migrations 
    /// and initialize missing table data on application startup.
    /// </summary>
    public static class DatabaseSeeder
    {
        /// <summary>
        /// Migrates pending schema changes to database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public static async Task SeedData<T>(WebApplication app) where T : IdentityDbContext<ApplicationUser, IdentityRole, string>
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<T>();
            await context.Database.MigrateAsync();
            await SeedRoles<T>(context);
        }

        /// <summary>
        /// Seeds roles table data in database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        private static async Task SeedRoles<T>(T context) where T : IdentityDbContext<ApplicationUser, IdentityRole, string>
        {
            string[] roles = { "Admin", "Receptionist", "Doctor" };
            var existingRoleNames = await context.Roles.Select(r => r.Name).ToListAsync();

            foreach (var role in roles.Except(existingRoleNames))
            {
                context.Roles.Add(new IdentityRole{ 
                    Name = role, 
                    NormalizedName = role?.ToUpper(), 
                    ConcurrencyStamp = Guid.NewGuid().ToString() 
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
