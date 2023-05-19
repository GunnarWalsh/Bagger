using bagger.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bagger.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }

    public DbSet<Warehouse> Warehouses { get; set; }

    public DbSet<WarehouseProduct> WarehouseProducts { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUserLogin<string>>()
        .HasKey(login => new { login.LoginProvider, login.ProviderKey });

        modelBuilder.Entity<WarehouseProduct>()
        .HasKey(wp => new { wp.WarehouseId, wp.ProductId });

        modelBuilder.Entity<WarehouseProduct>()
            .HasOne(wp => wp.Warehouse)
            .WithMany(w => w.WarehouseProducts)
            .HasForeignKey(wp => wp.WarehouseId);

        modelBuilder.Entity<WarehouseProduct>()
            .HasOne(wp => wp.Product)
            .WithMany(p => p.WarehouseProducts)
            .HasForeignKey(wp => wp.ProductId);
    }
}

