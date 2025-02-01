using Swashbuckle.AspNetCore.SwaggerUI;
using TestApp.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi(builder.Configuration);

builder.Services
    .AddSwagger(builder.Configuration)
    .AddValidators()
    .AddData()
    .AddDomain();

var app = builder.Build();

// app.InitMigrations();
//
// app.UsenExceptionMiddleware();
app.InitMigrations();
app.UseHttpsRedirection();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // options.DocExpansion(DocExpansion.None);
    // options.SwaggerEndpoint("/swagger/swagger.json", "API");
});
app.MapControllers();
app.Run();

