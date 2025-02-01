namespace DataSeeder.Models;

public sealed record PatientModel(PatientNameModel Name, GenderEnum Gender, DateTime BirthDate, bool Active);