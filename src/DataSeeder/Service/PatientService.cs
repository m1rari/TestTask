using DataSeeder.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DataSeeder.Services;

internal sealed class PatientService
{
    private readonly HttpClient _httpClient;
    private static readonly string uri= "https://localhost:7089/api/Patient/Create";  // Укажите свой API URL


    public PatientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PatientModel> Create(PatientModel model, CancellationToken token)
    {
        var json = JsonContent.Create(model);
        
        var response = await _httpClient.PostAsync(uri, json, token);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Не удалось отправить данные. Статус: {response.StatusCode}");
        }

        return model;
    }
}