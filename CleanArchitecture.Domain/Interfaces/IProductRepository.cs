using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);
}