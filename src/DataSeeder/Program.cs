using DataSeeder.Configuration;
using DataSeeder.Service;
using DataSeeder.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestApp.Constants;

Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<DataSeederService>();
        services.AddSingleton<ResourceService>();
        services.AddSingleton<DataGenerationService>();
        services.AddSingleton<PatientService>();

        services.Configure<ResourcePaths>(options =>
        {
            options.NamePath = ResoursePathConstants.Name;
            options.FamilyPath = ResoursePathConstants.Family;
            options.PatronymicPath = ResoursePathConstants.Patronymic;
        });

        services.AddHttpClient<PatientService>();
        services.AddLogging(builder => builder.AddConsole());
    })
    .Build()
    .Services
    .GetRequiredService<DataSeederService>()
    .Run();