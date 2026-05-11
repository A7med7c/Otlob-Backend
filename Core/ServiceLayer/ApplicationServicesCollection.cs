using Microsoft.Extensions.DependencyInjection;
using ServiceImplementation.MappingProfiles;
using SeviceAbstraction;

namespace ServiceImplementation;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IServicesManager, ServicesManager>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(ServiceImplementation.AssemblyReference).Assembly);
        });
        services.AddScoped<ImageResolver>();

        return services;
    }
}
