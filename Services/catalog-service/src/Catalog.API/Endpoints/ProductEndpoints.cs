using Catalog.API.Contracts;
using Catalog.Application.Products.Commands.ProductStatus;
using Catalog.Application.Products.Commands.UpdateProduct;
using Catalog.Application.Products.DeleteProduct;
using CatalogService.Application.Products.Commands.CreateProduct;
using CatalogService.Application.Products.Queries.GetProductById;
using CatalogService.Application.Products.Queries.GetProducts;
using MediatR;
using System.Reflection;

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

        group.MapPut("/{id:guid}", UpdateProduct);

        group.MapDelete("/{id:guid}", DeleteProduct);

        group.MapPatch("/{id:guid}/activate", ActivateProduct);

        group.MapPatch("/{id:guid}/archive", ArchiveProduct);

        return group;
    }

    private static async Task<IResult> ArchiveProduct(Guid id, ISender sender)
    {
        await sender.Send(new ArchiveProductCommand(id));

        return Results.NoContent();
    }

    private static async Task<IResult> ActivateProduct(Guid id, ISender sender)
    {
        await sender.Send(new ActivateProductCommand(id));

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteProduct(
    Guid id,
    ISender sender)
    {
        await sender.Send(
            new DeleteProductCommand(id));

        return Results.NoContent();
    }
    private static async Task<IResult> UpdateProduct(
    Guid id,
    UpdateProductRequest request,
    ISender sender)
    {
        await sender.Send(
            new UpdateProductCommand(
                id,
                request.Name,
                request.Price,
                "USD",
                request.Description));

        return Results.NoContent();
    }

    private static async Task<IResult> GetProducts(
    [AsParameters] GetProductsRequest request,
    ISender sender)
    {
        var result =
            await sender.Send(
                new GetProductsQuery(
                    request.Page,
                    request.PageSize,
                    request.Name,
                    request.Sku,
                    request.CategoryId,
                    request.SortBy,
                    request.Descending));

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
             Guid.Parse("11111111-1111-1111-1111-111111111111"),
            request.Description);

        Guid id = await sender.Send(command);

        return Results.Created(
            $"/api/products/{id}",
            new { id });


    }
}