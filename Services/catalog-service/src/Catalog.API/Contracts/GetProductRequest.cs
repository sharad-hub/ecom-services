namespace Catalog.API.Contracts
{
    public sealed class GetProductsRequest
    {
        public int Page { get; init; } = 1;

        public int PageSize { get; init; } = 20;

        public string? Name { get; init; }

        public string? Sku { get; init; }

        public Guid? CategoryId { get; init; }

        public string SortBy { get; init; } = "Name";

        public bool Descending { get; init; }
    }
}
