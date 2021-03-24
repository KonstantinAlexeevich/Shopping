using Domain.Foundation.Events;
using Domain.Foundation.EventSourcing;

namespace Shopping.Sales
{
    public static class OrderEvents
    {
        public interface IOrderEvent : IEvent
        {
        }
    }
    
    public class Order: Aggregate<string, OrderEvents.IOrderEvent>
    {
        private string _orderId;

        protected override void When(object evt)
        {
            throw new System.NotImplementedException();
        }

        public override string GetId() => _orderId;
    }
}