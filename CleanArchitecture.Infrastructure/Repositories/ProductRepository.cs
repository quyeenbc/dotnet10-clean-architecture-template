using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
        => await _context.Products
            .AnyAsync(p => p.Name.ToLower() == name.ToLower(), cancellationToken);
}