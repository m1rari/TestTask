using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApp.WebApi.Exception;
using System.Text.Json.Serialization;


namespace TestApp.WebApi.Extensions;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
         services.AddControllers(options =>
             {
                 options.Filters.Add<GlobalExceptionFilter>();
             })
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
             })
            .AddApplicationPart(typeof(IServiceCollectionExtension).Assembly)
            .AddControllersAsServices();
         
         return services;
    }
    
    
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen();
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
        });

        return services;
    }
    
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(IServiceCollectionExtension).Assembly);
        services.AddFluentValidationAutoValidation();
        
        return services;
    }
}