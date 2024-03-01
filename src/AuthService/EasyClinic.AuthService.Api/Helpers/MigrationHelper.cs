using Microsoft.EntityFrameworkCore;

namespace DMarket.Api.Helpers
{
    public static class MigrationHelper
    {
        public static async Task ApplyMigrationsIfAny<T>(WebApplication app) where T : DbContext
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<T>();
            await context.Database.MigrateAsync();
        }
    }
}
