using Domain.Foundation.Events;
using Shopping.Sales.Orders;

// ReSharper disable once CheckNamespace
namespace Shopping.Sales
{
    public static partial class NamedEvents
    {
        static void NameOrderEvents(this IEventMetadataMapper mapper) => mapper
            .Add<OrderEvents.OrderItemAdded>("OrderItemAdded")
            .Add<OrderEvents.OrderItemRemoved>("OrderItemRemoved");
    }
}