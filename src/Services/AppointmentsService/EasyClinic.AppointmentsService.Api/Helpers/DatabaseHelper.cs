using Microsoft.EntityFrameworkCore;

namespace EasyClinic.AppointmentsService.Api.Helpers
{
    /// <summary>
    /// Class for database initialization.
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>
        /// Applies migrations if any and seeds initial data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public static async Task InitializeData<T>(WebApplication app) where T : DbContext
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<T>();
            await context.Database.MigrateAsync();
        }
    }
}
