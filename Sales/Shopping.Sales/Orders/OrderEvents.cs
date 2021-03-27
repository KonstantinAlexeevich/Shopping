using Domain.Foundation.Events;

namespace Shopping.Sales.Orders
{
    public static class OrderEvents
    {
        public interface IOrderEvent : IEvent
        {
            public string OrderId { get; init; }
        }
        
        public class OrderItemAdded: EventBase, IOrderEvent
        {
            public string OrderId { get; init; }
            public long ProductId { get; init; }
            public uint Count { get; set; }
        }
        
        public class OrderItemRemoved: EventBase, IOrderEvent
        {
            public string OrderId { get; init; }
            public long ProductId { get; init; }
        }
        
    }
}