using Domain.Foundation.Events;
using Shopping.Sales.Orders;

// ReSharper disable once CheckNamespace
namespace Shopping.Sales
{
    public static partial class NamedEvents
    {
        public static void NameSalesEvents(this IEventMetadataMapper mapper) => 
            mapper.NameOrderEvents();
    }
}