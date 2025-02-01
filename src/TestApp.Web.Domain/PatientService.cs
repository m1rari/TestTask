using Microsoft.EntityFrameworkCore;
using TestApp.Common.Enum;
using TestApp.Storage;
using TestApp.Storage.Entities;
using TestApp.Web.Domain.Abstraction.Exceptions;
using TestApp.Web.Domain.Abstraction.Models;
using TestApp.Web.Domain.Abstraction.Services;

namespace TestApp.Web.Domain;

public class PatientService : IPatientService
{
    private readonly DataContext _context;
    
    public PatientService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<PatientModel[]> GetPatientsAsync(FilterPrefixEnum doBFilter, DateTime? doB, CancellationToken token)
    {
        var query = _context.Patients.AsNoTracking();

        if (doB.HasValue)
        {
            query = doBFilter switch
            {
                FilterPrefixEnum.eq => query.Where(e => e.DoB == doB.Value),
                FilterPrefixEnum.ne => query.Where(e => e.DoB != doB.Value),
                FilterPrefixEnum.gt => query.Where(e => e.DoB > doB.Value),
                FilterPrefixEnum.lt => query.Where(e => e.DoB < doB.Value),
                FilterPrefixEnum.ge => query.Where(e => e.DoB >= doB.Value),
                FilterPrefixEnum.le => query.Where(e => e.DoB <= doB.Value),
                _ => query
            };
        }

        var entities = await query
            .OrderByDescending(e => e.DoB)
            .ToArrayAsync(token);

        return Map(entities);
    }
    
    public async Task<PatientModel> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                     ?? throw new NotFoundException($"Пациент с ID {id} не найден");

        return Map(entity);
    }

    public async Task<PatientModel> Create(PatientModel model, CancellationToken token)
    {
        var id = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;
        var entity = new Patient
        {
            Id = id,
            Use = model.Name.Use?.Trim(),
            Name = model.Name.Given[0].Trim(),
            Family = model.Name.Family.Trim(),
            Patronymic = model.Name.Given[1].Trim(),
            Gender = (byte)model.Gender,
            DoB = model.BirthDate,
            IsActive = model.Active,
            
            
            CreatedAt = timestamp,
            UpdatedAt = timestamp
        };


        await _context.Patients.AddAsync(entity, token);
        await _context.SaveChangesAsync(token);

        return model with { Name = model.Name with { Id = id }};
    }
    
    public async Task Update(PatientModel model, CancellationToken token)
    {
        if (!model.Name.Id.HasValue)
        {
            throw new Exception();
        }

        var entity = await _context.Patients.FirstOrDefaultAsync(e => e.Id == model.Name.Id.Value, token)
                     ?? throw new NotFoundException($"Пациент с ID {model.Name.Id.Value} не найден");

        entity.Use = model.Name.Use?.Trim();
        entity.Name = model.Name.Given[0].Trim();
        entity.Family = model.Name.Family.Trim();
        entity.Patronymic = model.Name.Given[1].Trim();
        entity.Gender = (byte)model.Gender;
        entity.DoB = model.BirthDate;
        entity.IsActive = model.Active;
        entity.UpdatedAt = DateTime.UtcNow;


        _context.Patients.Update(entity);
        await _context.SaveChangesAsync(token);
    }
    
    public async Task Delete(Guid id, CancellationToken token)
    {
        var entity = await _context.Patients.FirstOrDefaultAsync(e => e.Id == id, token) 
                     ?? throw new NotFoundException($"Пациент с ID {id} не найден");

        _context.Patients.Remove(entity);
        await _context.SaveChangesAsync(token);
    }

    private static PatientModel[] Map(Patient[] entities) => entities.Select(Map).ToArray();

    private static PatientModel Map(Patient entity)
    {
        var nameModel = new PatientNameModel(entity.Id, entity.Use, entity.Family, new[] { entity.Name, entity.Patronymic });

        return new(nameModel, (GenderEnum)entity.Gender, entity.DoB, entity.IsActive);
    }
    
    
    
}