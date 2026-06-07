using E_Commerce.Web.Extentions;
using Persistence;
using ServiceImplementation;
<<<<<<< HEAD
=======
using System.Text.Json;
using System.Text.Json.Serialization;
>>>>>>> origin/Dev

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
<<<<<<< HEAD

            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();
            builder.Services.AddWebApplicationServices();
            builder.Services.AddApplicationServices();
=======
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            builder.Services.AddSwaggerServices();
            builder.Services.AddWebApplicationServices();
            builder.Services.AddApplicationServices(builder.Configuration);
>>>>>>> origin/Dev
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
<<<<<<< HEAD
=======
            app.UseCors("AllowAll");
>>>>>>> origin/Dev
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
