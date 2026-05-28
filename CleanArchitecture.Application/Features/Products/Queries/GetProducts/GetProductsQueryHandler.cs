using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProducts;

public class GetProductsQueryHandler
    : IRequestHandler<GetProductsQuery, Result<List<ProductDto>>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
        => _productRepository = productRepository;

    public async Task<Result<List<ProductDto>>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        var dtos = products.Select(p => new ProductDto(
            p.Id, p.Name, p.Description, p.Price, p.Stock, p.IsActive, p.CreatedAt
        )).ToList();

        return Result<List<ProductDto>>.Success(dtos);
    }
}