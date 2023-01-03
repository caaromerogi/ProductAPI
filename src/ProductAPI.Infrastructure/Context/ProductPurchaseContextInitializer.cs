using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Infrastructure.Context;

public class ProductPurchaseContextInitializer
{
    private readonly ProductPurchaseContext _context;
    public ProductPurchaseContextInitializer(ProductPurchaseContext context)
    {
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
           
            Console.WriteLine(ex.Message);
        }
    }
}