
using DomainLayer.Contracts;
using DomainLayer.Models.IdetityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using ServiceImplementation;
using ServiceImplementation.MappingProfiles;
using SeviceAbstraction;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            //Get Connection string
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            // rgister Seeder Service
            builder.Services.AddScoped<IDataSeeder, DataSeeder>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServicesManager, ServicesManager>();

            builder.Services.AddScoped<ImageResolver>();
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(ServiceImplementation.AssemblyReference).Assembly);
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();
            #endregion

            var app = builder.Build();

            #region Data Seeder Must be Before pipeline
            // create new scope 
            using (var scope = app.Services.CreateScope())
            {
                // create object from this scoope based on the regesterd in the DI Container
                var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
                // dosent return anything so sync vs async dosent change things exept making program working async
                await seeder.SeedIdentityDataAsync();
                await seeder.SeedDataAsync();
            }
            #endregion

            #region Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
