using E_Commerce.Web.Extentions;
using Persistence;
using ServiceImplementation;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container

            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();
            builder.Services.AddWebApplicationServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastuctureServices(builder.Configuration);
            builder.Services.AddJWTServices(builder.Configuration);
            #endregion

            var app = builder.Build();

            await app.SeedDataAsync();

            #region Configure the HTTP request pipeline.

            app.UseCustomExceptionMiddlewares();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
