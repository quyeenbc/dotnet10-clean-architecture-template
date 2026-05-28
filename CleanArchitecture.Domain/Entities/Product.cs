using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public bool IsActive { get; private set; } = true;

    private Product() { } // EF Core

    public static Product Create(string name, string? description, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name cannot be empty.");
        if (price < 0)
            throw new DomainException("Price cannot be negative.");
        if (stock < 0)
            throw new DomainException("Stock cannot be negative.");

        return new Product
        {
            Name = name,
            Description = description,
            Price = price,
            Stock = stock
        };
    }

    public void Update(string name, string? description, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name cannot be empty.");
        if (price < 0)
            throw new DomainException("Price cannot be negative.");

        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        SetUpdatedAt();
    }

    public void Deactivate() => IsActive = false;
}