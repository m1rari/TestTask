using TestApp.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi(builder.Configuration)
    .AddSwagger(builder.Configuration)
    .AddValidators()
    .AddData()
    .AddDomain();

var app = builder.Build();

app.InitMigrations();
app.UseHttpsRedirection();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();

