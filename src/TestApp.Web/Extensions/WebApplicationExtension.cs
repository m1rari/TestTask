using Microsoft.EntityFrameworkCore;
using TestApp.Storage;

namespace TestApp.WebApi.Extensions;

internal static class WebApplicationExtension
{
    public static void InitMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

        dataContext.Database.Migrate();
    }
}