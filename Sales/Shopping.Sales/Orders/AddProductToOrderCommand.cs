using System.Threading;
using System.Threading.Tasks;
using Domain.Foundation.CQRS;
using Shopping.Sales.Api.Orders;
using static Shopping.Sales.Api.Orders.AddProductToOrder;

namespace Shopping.Sales.Orders
{
    public class AddProductToOrderCommand: IAddProductToOrder, IAggregateCommandHandler<Order, string, Command, Result>
    {
        public Task<Result> ExecuteAsync(Order aggregate, Command command, CancellationToken cancellationToken)
        {
            aggregate.AddItem(new OrderItem()
            {
                ProductId = command.ProductId,
                Count = command.Count
            });

            return Task.FromResult(new Result());
        }
    }
}