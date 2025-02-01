using System.Reflection;
using System.Text.Json;
using DataSeeder.Configuration;
using DataSeeder.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace DataSeeder.Services;

internal sealed class ResourceService
{
    private readonly ILogger<ResourceService> _logger;
    public ResourceModel Name { get; }
    public ResourceModel Family { get; }
    public ResourceModel Patronymic { get; }

    public ResourceService(IOptions<ResourcePaths> options, ILogger<ResourceService> logger)
    {
        _logger = logger;

        var paths = options.Value;
        Name = LoadResource(paths.NamePath);
        Family = LoadResource(paths.FamilyPath);
        Patronymic = LoadResource(paths.PatronymicPath);
    }

    private ResourceModel LoadResource(string resourcePath)
    {
        var assembly = Assembly.GetExecutingAssembly();
       
        using var stream = assembly.GetManifestResourceStream(resourcePath);
        if (stream == null)
        {
            _logger.LogError("❌ Не удалось загрузить ресурс: {Path}", resourcePath);
            throw new FileNotFoundException($"Ресурс {resourcePath} не найден.");
        }

        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        return JsonSerializer.Deserialize<ResourceModel>(json) ?? throw new InvalidOperationException("Ошибка десериализации JSON.");
    }
}