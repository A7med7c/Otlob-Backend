
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;

namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
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
            // rgister Seeder Service
            builder.Services.AddScoped<IDataSeeder, DataSeeder>();
            #endregion

            var app = builder.Build();

            #region Data Seeder Must be Before pipeline
            // create new scope 
            using var Scoope = app.Services.CreateScope();
            // create object from this scoope based on the regesterd in the DI Container
            var seedingScopeObject = Scoope.ServiceProvider.GetRequiredService<IDataSeeder>();
            seedingScopeObject.SeedData();
            #endregion

            #region Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
