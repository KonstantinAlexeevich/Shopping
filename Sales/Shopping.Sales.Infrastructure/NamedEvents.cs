using Domain.Foundation.Events;

namespace Shopping.Sales.Infrastructure
{
    public static partial class NamedEvents
    {
        public static void NameSalesEvents(this IEventMetadataMapper mapper) => 
            mapper.NameOrderEvents();
    }
}