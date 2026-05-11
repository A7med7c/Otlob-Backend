using DomainLayer.Models.IdetityModule;
using E_Commerce.Web.Extentions;
using Microsoft.AspNetCore.Identity;
using Persistence;
using Persistence.Identity;
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
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            builder.Services.AddSwaggerServices();
            builder.Services.AddWebApplicationServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastuctureServices(builder.Configuration);
            #endregion

            var app = builder.Build();

            await app.SeedDataAsync();

            #region Configure the HTTP request pipeline.

            app.UseCustomExceptionMiddlewares();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
