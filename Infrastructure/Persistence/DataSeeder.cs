using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Text.Json;

namespace Persistence;

public class DataSeeder(ApplicationDbContext _dbContext) : IDataSeeder
{
    public void SeedData()
    {
        try
        {
            // check if there any migrations dosent applied 
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                //update db 
                _dbContext.Database.Migrate();
            }

            // check if ProductBrands have data or not 
            if (!_dbContext.ProductBrands.Any())
            {
                // reads as string
                var productBrandsData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\SeedingData\brands.json");
                //convert data "string" => C# Object -> desrialization
                var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);

                if (productBrands is not null && productBrands.Any())
                    _dbContext.ProductBrands.AddRange(productBrands);
            }

            if (!_dbContext.ProductTypes.Any())
            {
                var productTypesData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\SeedingData\types.json");
                var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                if (productTypes is not null && productTypes.Any())
                    _dbContext.ProductTypes.AddRange(productTypes);
            }
            if (!_dbContext.Products.Any())
            {
                var productsData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\SeedingData\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products is not null && products.Any())
                    _dbContext.Products.AddRange(products);
            }

            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            //

        }


    }
}
