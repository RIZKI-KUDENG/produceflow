using Microsoft.EntityFrameworkCore;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Asset> Assets => Set<Asset>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Asset>()
        .Property(a => a.Name)
        .IsRequired()
        .HasMaxLength(100);

        modelBuilder.Entity<Asset>()
        .Property(a => a.Price)
        .HasColumnType("decimal(18,2)");
    }
}