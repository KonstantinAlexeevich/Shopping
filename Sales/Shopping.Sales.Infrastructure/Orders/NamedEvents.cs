using Domain.Foundation.Events;
using Shopping.Sales.Orders;

// ReSharper disable once CheckNamespace
namespace Shopping.Sales.Infrastructure
{
    public static partial class NamedEvents
    {
        static void NameOrderEvents(this IEventMetadataMapper mapper) => mapper
            .Add<IOrderEvents.OrderItemAdded>("OrderItemAdded")
            .Add<IOrderEvents.OrderItemRemoved>("OrderItemRemoved");
    }
}