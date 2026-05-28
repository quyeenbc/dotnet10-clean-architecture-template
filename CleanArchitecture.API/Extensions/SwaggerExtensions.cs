using Microsoft.OpenApi;
namespace CleanArchitecture.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Clean Architecture API",
                Version = "v1",
                Description = "ASP.NET Core 10 — Clean Architecture Template with CQRS, MediatR, FluentValidation, PostgreSQL",
                Contact = new OpenApiContact
                {
                    Name = "Banh Cao Quyen",
                    Email = "quyeenbc@gmail.com"
                }
            });
        });

        return services;
    }
}