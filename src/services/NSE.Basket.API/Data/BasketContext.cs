using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.Basket.API.Models;

namespace NSE.Basket.API.Data;

public sealed class BasketContext : DbContext
{
    public BasketContext(DbContextOptions<BasketContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<BasketCustomer> BasketCustomers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<ValidationResult>();

        modelBuilder.Entity<BasketCustomer>()
            .HasIndex(c => c.CustomerId)
            .HasDatabaseName("IDX_Cliente");

        modelBuilder.Entity<BasketCustomer>()
            .HasMany(c => c.Items)
            .WithOne(i => i.BasketCustomer)
            .HasForeignKey(c => c.BasketId);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
    }
}
