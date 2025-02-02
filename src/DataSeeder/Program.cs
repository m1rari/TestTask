using DataSeeder.Configuration;
using DataSeeder.Service;
using DataSeeder.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestApp.Constants;

Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole(); 
        logging.SetMinimumLevel(LogLevel.Information); 
        logging.SetMinimumLevel(LogLevel.Error); 
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<ResourcePaths>(context.Configuration.GetSection("ResourcePaths"));
        services.Configure<ApiSettings>(context.Configuration.GetSection("ApiSettings"));
        
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