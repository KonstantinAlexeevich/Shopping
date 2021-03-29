using System.Linq;
using Domain.Foundation.Tactical;

namespace Shopping.Sales.Orders
{
    public partial class Order : IApplyOrderEvents
    {
        protected override void Apply(IOrderEvents evt) => (this as IApplyOrderEvents).ApplyEvent(evt);

        void IApply<IOrderEvents.OrderItemAdded>.Apply(IOrderEvents.OrderItemAdded evt)
        {
            _orderItems.Add(new OrderItem
            {
                ProductId = evt.ProductId,
                Count = evt.Count
            });
        }

        void IApply<IOrderEvents.OrderItemRemoved>.Apply(IOrderEvents.OrderItemRemoved evt)
        {
            var removed = _orderItems.Single(x => x.ProductId == evt.ProductId);
            _orderItems.Remove(removed);
        }
    }
}