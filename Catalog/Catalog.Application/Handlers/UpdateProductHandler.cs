using Catalog.Application.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class UpdateProductHandler: IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var statusUpdateProduct = _productRepository.UpdateProduct(new Product
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Summary = request.Summary,
            Price = request.Price,
            Brands = request.Brands,
            Types = request.Types
        });

        return statusUpdateProduct;
    }
}