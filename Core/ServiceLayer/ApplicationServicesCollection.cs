using DomainLayer.Models.SettingsModule;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceImplementation.MappingProfiles;
using SeviceAbstraction;

namespace ServiceImplementation;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IServicesManager, ServiceManagerWithFactoryDelegate>();

        services.AddScoped<Func<IProductService>>(provider =>
           () => provider.GetRequiredService<IProductService>());
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<Func<IOrderService>>(provider =>
        () => provider.GetRequiredService<IOrderService>());
        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<Func<IBasketService>>(provider =>
        () => provider.GetRequiredService<IBasketService>());
        services.AddScoped<IBasketService, BasketService>();

        services.AddScoped<Func<IAuthenticationService>>(provider =>
        () => provider.GetRequiredService<IAuthenticationService>());
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<Func<IPaymentService>>(provider =>
       () => provider.GetRequiredService<IPaymentService>());
        services.AddScoped<IPaymentService, PaymentService>();

        services.AddScoped<ICashService, CashService>();

        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<TwilioSettings>(configuration.GetSection("TwilioSettings"));

        services.AddTransient<INotificationsService, NotificationsService>();
        services.AddScoped<IFileStorageService, LocalFileStorageService>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(ServiceImplementation.AssemblyReference).Assembly);
        });
        services.AddScoped<ImageResolver>();
        services.AddScoped<OrderItemPictureUrlResolver>();


        return services;
    }
}
