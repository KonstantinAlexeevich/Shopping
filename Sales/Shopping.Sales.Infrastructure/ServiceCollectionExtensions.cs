using Domain.Foundation.EventStore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Sales.Orders;

namespace Shopping.Sales.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSalesAggregateStores(this IServiceCollection serviceCollection)
        {
            serviceCollection.StoreEventsAggregate<Order, string, IOrderEvents>();
        }
    }
}