using DomainLayer.Contracts;
using DomainLayer.Models.IdetityModule;
using DomainLayer.Models.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System.Text.Json;

namespace Persistence;

public class DataSeeder(ApplicationDbContext _dbContext,
            UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager,
            ApplicationIdentityDbContext _identityContext) : IDataSeeder
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task SeedDataAsync()
    {
        try
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await _dbContext.Database.MigrateAsync();

            var brandsSeededCorrectly = await _dbContext.ProductBrands
                .AnyAsync(b => b.Id == 1);

            if (!brandsSeededCorrectly)
            {
                // Wipe whatever partial data exists
                _dbContext.ProductBrands.RemoveRange(_dbContext.ProductBrands);
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[ProductBrands]', RESEED, 0)");

                var data = File.OpenRead(@"..\Infrastructure\Persistence\Data\SeedingData\brands.json");
                var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(data, _jsonOptions);
                if (brands is not null && brands.Any())
                {
                    await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [ProductBrands] ON");
                    await _dbContext.ProductBrands.AddRangeAsync(brands);
                    await _dbContext.SaveChangesAsync();
                    await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [ProductBrands] OFF");
                }
            }

            var typesSeededCorrectly = await _dbContext.ProductTypes
                .AnyAsync(t => t.Id == 1);

            if (!typesSeededCorrectly)
            {
                _dbContext.ProductTypes.RemoveRange(_dbContext.ProductTypes);
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('[ProductTypes]', RESEED, 0)");

                var data = File.OpenRead(@"..\Infrastructure\Persistence\Data\SeedingData\types.json");
                var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(data, _jsonOptions);
                if (types is not null && types.Any())
                {
                    await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [ProductTypes] ON");
                    await _dbContext.ProductTypes.AddRangeAsync(types);
                    await _dbContext.SaveChangesAsync();
                    await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [ProductTypes] OFF");
                }
            }

            if (!await _dbContext.Products.AnyAsync())
            {
                var data = File.OpenRead(@"..\Infrastructure\Persistence\Data\SeedingData\products.json");
                var products = await JsonSerializer.DeserializeAsync<List<Product>>(data, _jsonOptions);
                if (products is not null && products.Any())
                {
                    await _dbContext.Products.AddRangeAsync(products);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seeding error: {ex.Message}");
            throw;
        }
    }

    public async Task SeedIdentityDataAsync()
    {
        try
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if (!_userManager.Users.Any())
            {
                var userOne = new ApplicationUser()
                {
                    Email = "Ahmed@gmail.com",
                    DisplayName = "Ahmed Ragab",
                    UserName = "AhmedRagab",
                    PhoneNumber = "0123456789"
                };
                var userTwo = new ApplicationUser()
                {
                    Email = "Abdo@gmail.com",
                    DisplayName = "Abdo Ragab",
                    UserName = "AbdoRagab",
                    PhoneNumber = "0123456789"
                };
                await _userManager.CreateAsync(userOne, "A7med_123");
                await _userManager.CreateAsync(userTwo, "A7med_123");
            }

            await _identityContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
        }

    }
}