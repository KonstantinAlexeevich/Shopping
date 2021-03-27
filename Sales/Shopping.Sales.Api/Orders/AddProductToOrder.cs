
using Domain.Foundation.CQRS;

namespace Shopping.Sales.Api.Orders
{
    public interface IAddProductToOrder : IHandler<AddProductToOrder.Command, AddProductToOrder.Result> 
    { }
    
    public static class AddProductToOrder
    {
        public record Command: ICommand<string>
        {
            public string AggregateId { get; init; }
            public long ProductId { get; init; }
            public uint Count { get; init; }
        }
        
        public record Result
        { }
    }
}