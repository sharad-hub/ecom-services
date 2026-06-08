using Catalog.API.Contracts;
using CatalogService.Application.Products.Commands.CreateProduct;
using CatalogService.Application.Products.Queries.GetProductById;
using CatalogService.Application.Products.Queries.GetProducts;
using MediatR;

namespace Catalog.API.Endpoints;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products");

        group.MapPost("/", CreateProduct);

        group.MapGet("/{id:guid}", GetProductById);

        group.MapGet("/", GetProducts);


        return group;
    }

    private static async Task<IResult> GetProducts(
    ISender sender)
    {
        var result = await sender.Send(
            new GetProductsQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetProductById(
    Guid id,
    ISender sender)
    {
        var result = await sender.Send(
            new GetProductByIdQuery(id));

        return Results.Ok(result);
    }
    private static async Task<IResult> CreateProduct(
        CreateProductRequest request,
        ISender sender)
    {
        var command = new CreateProductCommand(
            request.Name,
            request.Sku,            
            request.Price,
            "USD",
            request.CategoryId,
            request.Description);

        Guid id = await sender.Send(command);

        return Results.Created(
            $"/api/products/{id}",
            new { id });


    }
}