using TestApp.Common.Enum;
using TestApp.Web.Domain.Abstraction.Models;

namespace TestApp.Web.Domain.Abstraction.Services;

public interface IPatientService
{
    Task<PatientModel> Get(Guid id, CancellationToken token);

    Task<PatientModel[]> GetPatientsAsync(FilterPrefixEnum doBFilter, DateTime? doB, CancellationToken token);

    Task<PatientModel> Create(PatientModel model, CancellationToken token);

    Task Update(PatientModel model, CancellationToken token);

    Task Delete(Guid id, CancellationToken token);
}