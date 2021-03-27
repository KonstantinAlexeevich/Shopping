using System.Collections.Generic;

namespace Shopping.Sales.Storage.EntityFramework
{
    public class Order
    {
        public long Id { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public string CreateUserId { get; set; }
    }
}