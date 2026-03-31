using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Text.Json;

namespace Persistence;

public class DataSeeder(ApplicationDbContext _dbContext) : IDataSeeder
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

            // ✅ Check if brands exist WITH correct IDs, not just "any rows"
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
}