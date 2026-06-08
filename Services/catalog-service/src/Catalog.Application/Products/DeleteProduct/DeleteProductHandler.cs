using CatalogService.Application.Abstractions.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Products.DeleteProduct
{
    public sealed class DeleteProductHandler
      : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductHandler(
            IProductRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            DeleteProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (product is null)
            {
                throw new Exception("Product not found");
            }

            product.Delete();

            //Since EF Core tracks entities returned by GetByIdAsync(), so we usually don't even need:
            //await _repository.UpdateAsync(
            //     product,
            //     cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }
    }
}
