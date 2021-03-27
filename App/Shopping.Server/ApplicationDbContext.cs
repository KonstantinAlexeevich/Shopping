using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopping.Sales.Storage.EntityFramework;

namespace Shopping.Server
{
  public class ApplicationDbContext : IdentityDbContext, ISalesContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; init; }
    public DbSet<OrderItem> OrderItems { get; init; }
    public DbSet<Product> Products { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        this.ApplyConfigurations(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
  }
}
