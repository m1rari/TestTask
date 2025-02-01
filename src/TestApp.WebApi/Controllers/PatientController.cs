using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using TestApp.Web.Domain.Abstraction.Models;
using TestApp.Web.Domain.Abstraction.Services;
using TestApp.WebApi.Parser;

namespace TestApp.WebApi.Controllers;

[Tags("Пациенты")]
[ApiController]
[Route("api/[controller]/[action]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _service;

    public PatientController(IPatientService service)
    {
        _service = service;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Получение списка пациента с опциональным параметром фильтра по дате рождения")]
    [SwaggerResponse(StatusCodes.Status200OK,                  ContentTypes = new[] { MediaTypeNames.Application.Json }, Type = typeof(PatientModel[]))]
    [SwaggerResponse(StatusCodes.Status400BadRequest,          ContentTypes = new[] { MediaTypeNames.Application.Json }, Type = typeof(ValidationProblemDetails))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, ContentTypes = new[] { MediaTypeNames.Application.Json })]
    public async Task<ActionResult<PatientModel[]>> Get(string? birthDate, CancellationToken token)
    {
        // Разбираем входной параметр
        var (prefix, dateFilter) = DateWithFilterParser.Parse(birthDate);

        // Получаем пациентов по фильтру
        var patients = await _service.GetPatientsAsync(prefix, dateFilter, token);
        return Ok(patients);
    }
    
    [HttpGet, Route("{id:guid}")]
    [SwaggerOperation(Summary = "Получение пациента по id")]
    public async Task<ActionResult<PatientModel>> Get(Guid id, CancellationToken token)
    {
        var model = await _service.Get(id, token);
        return Ok(model);
    }
    
    
    [HttpPost]
    [SwaggerOperation(Summary = "Создание записи о пациенте")]
    public async Task<ActionResult<PatientModel>> Create(PatientModel model, CancellationToken token)
    {
        var createdModel = await _service.Create(model, token);

        return Created($"api/v1/patients/{createdModel.Name.Id}", createdModel);
    }
    
    [HttpPut]
    [SwaggerOperation(Summary = "Обновление записи о пациенте")]
    [SwaggerResponse(StatusCodes.Status200OK,                  ContentTypes = new[] { MediaTypeNames.Application.Json }, Type = typeof(PatientModel))]
    [SwaggerResponse(StatusCodes.Status400BadRequest,          ContentTypes = new[] { MediaTypeNames.Application.Json }, Type = typeof(ValidationProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound,            ContentTypes = new[] { MediaTypeNames.Application.Json })]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, ContentTypes = new[] { MediaTypeNames.Application.Json })]
    public async Task<ActionResult<PatientModel>> Update(PatientModel model, CancellationToken token)
    {
        await _service.Update(model, token);

        return Ok(model);
    }

    [HttpDelete, Route("{id:guid}")]
    [SwaggerOperation(Summary = "Удаление записи о пациенте по id")]
    [SwaggerResponse(StatusCodes.Status204NoContent,           ContentTypes = new[] { MediaTypeNames.Application.Json })]
    [SwaggerResponse(StatusCodes.Status404NotFound,            ContentTypes = new[] { MediaTypeNames.Application.Json })]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, ContentTypes = new[] { MediaTypeNames.Application.Json })]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await _service.Delete(id, token);

        return NoContent();
    }
}