using Microsoft.EntityFrameworkCore;

namespace EasyClinic.AuthService.Api.Helpers
{
    /// <summary>
    /// Helper class to automatically apply migrations on application startup.
    /// </summary>
    public static class AutoMigrationHelper
    {
        /// <summary>
        /// Migrates pending schema changes to database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public static async Task ApplyMigrationsIfAny<T>(WebApplication app) where T : DbContext
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<T>();
            await context.Database.MigrateAsync();
        }
    }
}
