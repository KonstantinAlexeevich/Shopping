using System;
using System.Collections.Generic;
using Domain.Foundation.Tactical;
using static Shopping.Sales.Orders.OrderEvents;

namespace Shopping.Sales.Orders
{
    public class Order : EventsAggregate<string, IOrderEvent>
    {
        private readonly string _orderId;
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();

        public Order(string orderId)
        {
            _orderId = orderId ?? Guid.NewGuid().ToString();
        }
        
        public override string GetId() => _orderId;

        public void AddItem(OrderItem orderItem)
        {
            Apply(new OrderItemAdded
            {
                OrderId = _orderId,
                ProductId = orderItem.ProductId,
                Count = orderItem.Count
            });
        }

        protected override void When(IOrderEvent evt)
        {
            switch (evt)
            {
                case OrderItemAdded x:
                    
                    _orderItems.Add(new OrderItem()
                    {
                        ProductId = x.ProductId,
                        Count = x.Count
                    });
                    
                    break;
                
                case OrderItemRemoved orderItemRemoved:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(evt));
            }
        }
    }
}