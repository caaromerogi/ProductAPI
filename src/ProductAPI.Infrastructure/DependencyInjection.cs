using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductAPI.Infrastructure.Context;

namespace ProductAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddDbContext<ProductPurchaseContext>(options => 
        options.UseSqlServer("Server=PSOFKA0975\\SQLEXPRESS;Database=ProductAPI;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True",
        builder => builder.MigrationsAssembly(typeof(ProductPurchaseContext).Assembly.FullName)));
        
        services.AddScoped<ProductPurchaseContextInitializer>();

        return services;
    }
}