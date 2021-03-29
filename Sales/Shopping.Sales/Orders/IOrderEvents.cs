using Domain.Foundation;
using Domain.Foundation.Events;

namespace Shopping.Sales.Orders
{
    [AutoApplyInterface]
    public interface IOrderEvents: IEvent
    {
        public string OrderId { get; init; }
        
        public class OrderItemAdded: EventBase, IOrderEvents
        {
            public string OrderId { get; init; }
            public long ProductId { get; init; }
            public uint Count { get; set; }
        }
        
        public class OrderItemRemoved: EventBase, IOrderEvents
        {
            public string OrderId { get; init; }
            public long ProductId { get; init; }
        }
    }
}