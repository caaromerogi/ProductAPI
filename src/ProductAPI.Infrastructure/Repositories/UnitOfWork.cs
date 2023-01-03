using ProductAPI.Application.Common.Interfaces;
using ProductAPI.Domain.Models;
using ProductAPI.Infrastructure.Context;

namespace ProductAPI.Infrastructure.Repositories;

public class unitOfWork : IUnitOfWork
{
    private readonly ProductPurchaseContext _context;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IGenericRepository<Purchase> _purchaseRepository;
    public unitOfWork(ProductPurchaseContext context, IGenericRepository<Product> productRepository)
    {
        _context = context;
        _productRepository = productRepository;
    }

    public IGenericRepository<Product> ProductRepository => _productRepository ?? new GenericRepository<Product>(_context);

    public IGenericRepository<Purchase> PurchaseRepository => _purchaseRepository ?? new GenericRepository<Purchase>(_context);

    public void Dispose()
    {
        if(_context != null){
            _context.Dispose();
        }
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}