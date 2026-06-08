 

namespace Catalog.Infrastructure.Persistence
{
    using CatalogService.Domain.Aggregates;
    using CatalogService.Domain.ValueObjects;

 
    public static class CatalogDbContextSeeder
    {
        public static async Task SeedAsync(
            CatalogDbContext context)
        {
            if (context.Products.Any())
            {
                return;
            }

            var brands = new[]
                        {
                            "Apple",
                            "Samsung",
                            "Dell",
                            "HP",
                            "Lenovo",
                            "Asus",
                            "Sony",
                            "LG",
                            "OnePlus",
                            "Xiaomi"
                        };

            var categoryId =
                Guid.Parse("11111111-1111-1111-1111-111111111111");

            var products = new List<Product>();

            int counter = 1;
             foreach (var brand in brands)
            {
                for (int i = 1; i <= 5; i++)
                { 
                    
                    var product =
                        Product.Create(
                            $"{brand} Product {i}",
                            //$"{brand[..3].ToUpper()}{counter:000}",
                            $"{brand.Substring(0, Math.Min(3, brand.Length)).ToUpper()}{counter:000}",
                            Money.Create(
                                Random.Shared.Next(500, 3000),
                                "USD").Value ,
                            categoryId,
                            $"{brand} sample product")
                        .Value;

                    products.Add(product);

                    counter++;
                }
            }

            await context.Products.AddRangeAsync(products);

            await context.SaveChangesAsync();


        }
    }
}
