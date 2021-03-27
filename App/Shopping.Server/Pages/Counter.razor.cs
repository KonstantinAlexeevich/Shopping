using System.Threading;
using System.Threading.Tasks;
using Domain.Foundation;
using Domain.Foundation.Api;
using Microsoft.AspNetCore.Components;
using Shopping.Sales.Api.Orders;

namespace Shopping.Server.Pages
{
    public partial class Counter
    {
        private string _error;
        private AddProductToOrder.Result _result;
        private string _inputValue;

        [Inject]
        public IApiHandler<AddProductToOrder.Command, AddProductToOrder.Result> AddProductToOrder { get; set; }
    
        private async Task IncrementCount()
        {
            var command = new AddProductToOrder.Command()
            {
                AggregateId = _inputValue,
                Count = 1,
                ProductId = 1
            };
            
            await AddProductToOrder
                .Handle(command, CancellationToken.None)
                .OnError(x => _error = x)
                .OnSuccess(x => _result = x);
        }
    }
}