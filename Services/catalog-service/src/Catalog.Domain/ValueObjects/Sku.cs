using System.Text.RegularExpressions;
using CatalogService.Domain.Common;
using CatalogService.Domain.Errors;

namespace CatalogService.Domain.ValueObjects;

public sealed record Sku
{
    public string Value { get; }

    private Sku()
    {
        
    }
    private Sku(string value)
    {
        Value = value;
    }

    public static Result<Sku> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<Sku>.Failure(ProductErrors.InvalidSku);
        }

        var normalized = value.Trim().ToUpperInvariant();

        var regex = new Regex(@"^[A-Z0-9-]{3,20}$");

        if (!regex.IsMatch(normalized))
        {
            return Result<Sku>.Failure(ProductErrors.InvalidSku);
        }

        return Result<Sku>.Success(new Sku(normalized));
    }

    public override string ToString()
    {
        return Value;
    }
}