using DataSeeder.Services;
using Microsoft.Extensions.Logging;

namespace DataSeeder.Service;

internal sealed class DataSeederService
{
    private readonly DataGenerationService _dataGenerationService;
    private readonly PatientService _patientService;
    private readonly ILogger<DataSeederService> _logger;

    public DataSeederService(
        DataGenerationService dataGenerationService, 
        PatientService patientService,
        ILogger<DataSeederService> logger)
    {
        _dataGenerationService = dataGenerationService;
        _patientService = patientService;
        _logger = logger;
    }

    public void Run()
    {
        _logger.LogInformation("🔄 Начало генерации данных...");

        var patients = _dataGenerationService.Generate(100);

        _logger.LogInformation("✅ Генерация завершена! Отправка данных на API...");

        var options = new ParallelOptions { MaxDegreeOfParallelism = 3 };

        var task = Parallel.ForEachAsync(patients, options, async (patient, token) =>
        {
            await _patientService.Create(patient, token);
            _logger.LogInformation("📤 Отправлен пациент: {Name} {Family}", 
                patient.Name.Given[0], patient.Name.Family);
        });

        task.Wait();

        _logger.LogInformation("🎉 Завершено!");
    }
}