using Shopping.Sales.Storage.Abstractions;

namespace Shopping.Storage.Abstractions
{
    public class OrderItem
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public Order Order { get; set; }
        
        public Product Product { get; set; }
    }
}