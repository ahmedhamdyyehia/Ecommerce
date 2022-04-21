using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context,ILoggerFactory LoggerFactory)
        {
            try
            {
                if(!context.ProductBrands.Any())
                {
                    var BrandsData =File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands=JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
                
                if(!context.ProductTypes.Any())
                {
                    var TypesData =File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types=JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if(!context.products.Any())
                {
                    var ProductsData =File.ReadAllText("../Infrastructure/Data/SeedData/Products.json");
                    var Products=JsonSerializer.Deserialize<List<product>>(ProductsData);
                    foreach (var item in Products)
                    {
                        context.products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch(Exception ex)
            {
                var logger=LoggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);

            }
        }
    }

}