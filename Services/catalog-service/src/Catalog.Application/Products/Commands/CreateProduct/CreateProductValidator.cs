using FluentValidation;

namespace CatalogService.Application.Products.Commands.CreateProduct;

public sealed class CreateProductValidator
    : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Sku)
            .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.CategoryId)
            .NotEqual(Guid.Empty);
    }
}