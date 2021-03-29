using System;
using System.Collections.Generic;
using Domain.Foundation.Tactical;
using static Shopping.Sales.Orders.IOrderEvents;

namespace Shopping.Sales.Orders
{
    public partial class Order : EventsAggregate<string, IOrderEvents>
    {
        private readonly string _orderId;
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public override string GetId() => _orderId;
        
        public Order(string orderId)
        {
            _orderId = orderId ?? Guid.NewGuid().ToString();
        }

        public void AddItem(OrderItem orderItem)
        {
            Emit(new OrderItemAdded
            {
                OrderId = _orderId,
                ProductId = orderItem.ProductId,
                Count = orderItem.Count
            });
        }
    }
    
}