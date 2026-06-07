using System.Text.Json;
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace E_Commerce.Web.Extentions;

public static class WebApplicationRegistration
{
    public static async Task SeedDataAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            await seeder.SeedDataAsync();
            await seeder.SeedIdentityDataAsync();
        }
    }
    public static IApplicationBuilder UseCustomExceptionMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomExceptionHandlerMiddleware>();
        return app;
    }
    public static IApplicationBuilder UseSwaggerMiddlewares(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(Options =>
{
    Options.ConfigObject = new ConfigObject()
    {
        DisplayRequestDuration = true
    };

    Options.DocumentTitle = "Hatley";

    Options.JsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    Options.DocExpansion(DocExpansion.None);
    Options.EnableFilter();
    Options.EnablePersistAuthorization();
});
        return app;
    }
}
