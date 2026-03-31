using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Text.Json;

namespace Persistence;

public class DataSeeder(ApplicationDbContext _dbContext) : IDataSeeder
{
    public async Task SeedDataAsync()
    {
        try
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            // check if there any migrations dosent applied 
            if (pendingMigrations.Any())
            {
                //update db 
                await _dbContext.Database.MigrateAsync();
            }

            // check if ProductBrands have data or not 
            if (!_dbContext.ProductBrands.Any())
            {
                // reads as string - DeserializeAsync take stream object so we use OpenRead
                var productBrandsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\SeedingData\brands.json");
                //convert data "string" => C# Object -> desrialization
                var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);

                if (productBrands is not null && productBrands.Any())

                    //AddRange and AddRangeAsync are the same if there is no operations depending on it.
                    await _dbContext.ProductBrands.AddRangeAsync(productBrands);
            }

            if (!_dbContext.ProductTypes.Any())
            {
                var productTypesData = File.OpenRead(@"..\Infrastructure\Persistence\Data\SeedingData\types.json");
                var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypesData);
                if (productTypes is not null && productTypes.Any())
                    await _dbContext.ProductTypes.AddRangeAsync(productTypes);
            }
            if (!_dbContext.Products.Any())
            {
                var productsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\SeedingData\products.json");
                var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);

                if (products is not null && products.Any())
                    await _dbContext.Products.AddRangeAsync(products);
            }

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //

        }


    }
}
