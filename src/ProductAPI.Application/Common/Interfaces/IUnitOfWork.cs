using ProductAPI.Domain.Models;

namespace ProductAPI.Application.Common.Interfaces;

public interface IUnitOfWork  : IDisposable
{
    IGenericRepository<Product> ProductRepository {get;}
    IGenericRepository<Purchase> PurchaseRepository {get;}
    IGenericRepository<ProductPurchase> ProductPurchaseRepository{get;}
    void SaveChanges();
    Task SaveChangesAsync();
}