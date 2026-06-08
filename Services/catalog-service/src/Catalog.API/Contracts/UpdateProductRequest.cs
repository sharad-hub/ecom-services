namespace Catalog.API.Contracts
{
    public class UpdateProductRequest
    {
    }

    public record UpdateProductRequest(
    string Sku,
    string Name,
    decimal Price,
    Guid CategoryId,
    string Description);
}
