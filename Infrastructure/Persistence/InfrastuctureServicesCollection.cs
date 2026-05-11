using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Persistence;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastuctureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Get Connection string
        services.AddDbContext<ApplicationDbContext>(options =>
         {
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
         });

        services.AddDbContext<ApplicationIdentityDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
        });
        // rgister Seeder Service
        services.AddScoped<IDataSeeder, DataSeeder>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddSingleton<IConnectionMultiplexer>((_) =>
        {
            return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
        });
        return services;
    }
}
