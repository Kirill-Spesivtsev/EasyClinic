using Microsoft.EntityFrameworkCore;

namespace EasyClinic.OfficesService.Api.Helpers
{
    /// <summary>
    /// Class for database initialization.
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Initializes database if it doesn't exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public static async Task Initialize<T>(WebApplication app) where T : DbContext
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<T>();
            await context.Database.EnsureCreatedAsync();
        }
    }
}
