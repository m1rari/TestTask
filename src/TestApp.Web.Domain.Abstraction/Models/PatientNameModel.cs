namespace TestApp.Web.Domain.Abstraction.Models;

public record PatientNameModel(Guid? Id, string? Use, string Family, string[] Given);