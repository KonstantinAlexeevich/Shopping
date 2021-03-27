using Microsoft.EntityFrameworkCore;
using Shopping.Sales.Storage.EntityFramework.Configurations;

namespace Shopping.Sales.Storage.EntityFramework
{
    public static class SalesContextExtensions
    {
        public static void ApplyConfigurations(this ISalesContext context, ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
    
    public interface ISalesContext
    {
        public DbSet<Order> Orders { get; init; }
        public DbSet<OrderItem> OrderItems { get; init; }
        public DbSet<Product> Products { get; init; }
    }
}