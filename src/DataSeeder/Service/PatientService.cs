using DataSeeder.Configuration;
using DataSeeder.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DataSeeder.Services;

internal sealed class PatientService
{
    private  HttpClient _httpClient;
    private readonly string _uri;

    public PatientService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
    {
        var handler = new HttpClientHandler()
        {
            // только в рамках тестового задания и тестирования
            ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
            {
                return true;
            }
        };
        _httpClient = httpClient;
       
        _uri = apiSettings.Value.PatientCreateUri;
    }

    public async Task<PatientModel> Create(PatientModel model, CancellationToken token)
    {
        var json = JsonContent.Create(model);
   
        var response = await _httpClient.PostAsync(_uri, json, token);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Не удалось отправить данные. Статус: {response.StatusCode}");
        }

        return model;
    }
}