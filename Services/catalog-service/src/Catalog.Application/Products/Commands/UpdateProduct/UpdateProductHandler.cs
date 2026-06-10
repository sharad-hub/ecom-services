using Catalog.Domain.Contract;
using CatalogService.Application.Abstractions.Persistence;
using CatalogService.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductHandler
       : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductCacheService _cacheService;

        public UpdateProductHandler(
            IProductRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (product is null)
            {
                throw new Exception("Product not found");
            }

            var money = Money.Create(
             request.Price,
             request.Currency).Value;

            product.Update(
                request.Name,
                money,
                request.Description);

            await _repository.UpdateAsync(
                product,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
            await _cacheService.RemoveAsync(product.Id);
        }
    }
}
