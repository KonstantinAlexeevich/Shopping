using System.Collections.Generic;
using Shopping.Storage.Abstractions;

namespace Shopping.Sales.Storage.Abstractions
{
    public class Order
    {
        public long Id { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public string CreateUserId { get; set; }
    }
}