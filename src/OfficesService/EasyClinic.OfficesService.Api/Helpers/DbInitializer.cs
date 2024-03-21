using Microsoft.EntityFrameworkCore;

namespace EasyClinic.OfficesService.Api.Helpers
{
    public static class DbInitializer
    {
        public static async Task Initialize<T>(WebApplication app) where T : DbContext
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<T>();
            await context.Database.EnsureCreatedAsync();
        }
    }
}
