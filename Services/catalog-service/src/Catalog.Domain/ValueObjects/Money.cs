using CatalogService.Domain.Common;
using CatalogService.Domain.Errors;

namespace CatalogService.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }

    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Result<Money> Create(
        decimal amount,
        string currency = "USD")
    {
        if (amount <= 0)
        {
            return Result<Money>.Failure(ProductErrors.InvalidPrice);
        }

        return Result<Money>.Success(
            new Money(amount, currency));
    }
}