using Microsoft.EntityFrameworkCore;

namespace EasyClinic.AuthService.Api.Helpers
{
    public static class AutoMigrationHelper
    {
        public static async Task ApplyMigrationsIfAny<T>(WebApplication app) where T : DbContext
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<T>();
            await context.Database.MigrateAsync();
        }
    }
}
