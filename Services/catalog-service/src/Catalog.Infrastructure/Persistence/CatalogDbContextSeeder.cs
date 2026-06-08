

namespace Catalog.Infrastructure.Persistence
{
    using Catalog.Domain.Aggregates;
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

            var categories = new List<Category>
{
    new(
        Guid.Parse("11111111-1111-1111-1111-111111111111"),
        "Electronics",
        "General electronics"),

    new(
        Guid.Parse("22222222-2222-2222-2222-222222222222"),
        "Smartphones",
        "Smart phones"),

    new(
        Guid.Parse("33333333-3333-3333-3333-333333333333"),
        "Laptops",
        "Laptop computers"),

    new(
        Guid.Parse("44444444-4444-4444-4444-444444444444"),
        "Accessories",
        "Accessories"),

    new(
        Guid.Parse("55555555-5555-5555-5555-555555555555"),
        "Gaming",
        "Gaming products")
};

            await context.Categories.AddRangeAsync(categories);

            await context.SaveChangesAsync();

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

            //var categoryId =
            //    Guid.Parse("11111111-1111-1111-1111-111111111111");

            var products = new List<Product>();

 
            int counter = 1;
            var categoryIds = new[]
{
    Guid.Parse("11111111-1111-1111-1111-111111111111"),
    Guid.Parse("22222222-2222-2222-2222-222222222222"),
    Guid.Parse("33333333-3333-3333-3333-333333333333"),
    Guid.Parse("44444444-4444-4444-4444-444444444444"),
    Guid.Parse("55555555-5555-5555-5555-555555555555")
};
            foreach (var brand in brands)
            {
                for (int i = 1; i <= 10; i++)
                {
                    var categoryId =
                        categoryIds[Random.Shared.Next(categoryIds.Length)];

                    var sku =
                        $"{brand.ToUpper().Replace(" ", "")}-{counter:000}";

                    var result =
                        Product.Create(
                            $"{brand} Product {i}",
                            sku,
                            Money.Create(
                                Random.Shared.Next(500, 5000),
                                "USD").Value,
                            categoryId,
                            $"{brand} sample product")
                        ;

                    if (result.IsSuccess)
                    {
                        products.Add(result.Value);
                    }

                    counter++;
                }
            }


            await context.Products.AddRangeAsync(products);

            await context.SaveChangesAsync();


        }
    }
}
