using Microsoft.EntityFrameworkCore;
using TestApp.Storage;
using TestApp.Web.Domain;
using TestApp.Web.Domain.Abstraction.Services;

namespace TestApp.WebApi.Extensions;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>((provider, options) =>
        {
            using var scope = provider.CreateScope();
            var configuration = scope.ServiceProvider.GetService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("Data");
        
            options.UseNpgsql(connectionString);
        });

        return services;
    }

    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IPatientService, PatientService>();

        return services;
    }
}