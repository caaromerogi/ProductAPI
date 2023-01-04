using Microsoft.EntityFrameworkCore;
using ProductAPI.Domain.Models;

namespace ProductAPI.Infrastructure.Context;

public class ProductPurchaseContext : DbContext
{
    public ProductPurchaseContext(DbContextOptions options) : base(options)
    {
    }

    protected ProductPurchaseContext()
    {
    }

    public DbSet<Product> Products {get; set;}
    public DbSet<Purchase> Purchases {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Purchase>().ToTable("Purchase");

        modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
        modelBuilder.Entity<Purchase>().HasKey(p => p.PurchaseId);

        modelBuilder.Entity<Product>()
        .HasMany(p => p.Purchases)
        .WithMany(p => p.Products)
        .UsingEntity<ProductPurchase>(pp => {
            pp.HasOne(pp=> pp.Product)
            .WithMany(p => p.ProductPurchases)
            .HasForeignKey(pp => pp.ProductId);
            pp.HasOne(pp => pp.Purchase)
            .WithMany(p => p.ProductPurchases)
            .HasForeignKey(pp => pp.PurchaseId);
            pp.HasKey( k => new {k.ProductId, k.PurchaseId});
        }
        );
   

        /*
        modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
        modelBuilder.Entity<Purchase>().HasKey(p => p.PurchaseId);

        modelBuilder.Entity<ProductPurchase>()
        .HasKey(pp => new {pp.ProductId, pp.PurchaseId});
        modelBuilder.Entity<ProductPurchase>()
        .HasOne(pp => pp.Product)
        .WithMany(p => p.ProductPurchases)
        .HasForeignKey(pp => pp.ProductId);

        modelBuilder.Entity<ProductPurchase>()
        .HasOne(pp =>pp.Purchase)
        .WithMany(p => p.ProductPurchases)
        .HasForeignKey(pp => pp.PurchaseId);
        */
    }

    
}