namespace CatalogService.Domain.Errors;

public static class ProductErrors
{
    public const string NameRequired =
        "Product name is required.";

    public const string InvalidSku =
        "SKU format is invalid.";

    public const string InvalidPrice =
        "Price must be greater than zero.";

    public const string CategoryRequired =
        "Category is required.";

    public const string ProductInactive =
        "Product is inactive.";
}