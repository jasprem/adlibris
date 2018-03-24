using Catalog.CommandProcessors;
using Catalog.Commands;
using Catalog.WebApi.Contracts;

namespace Catalog.Application.Services
{
    public class CheckoutService
    {
        private readonly OrderCommandHandler _orderCommandHandler;

        public CheckoutService(
            OrderCommandHandler orderCommandHandler)
        {
            _orderCommandHandler = orderCommandHandler;
        }

        public void Checkout(CheckoutParameter checkoutParameter)
        {
            foreach (var product in checkoutParameter.CartParameter.Products)
            {
                var orderCommand = new OrderCommand
                {
                    CustomerId = checkoutParameter.CustomerId,
                    CartId = checkoutParameter.CartParameter.CartId,
                    ProductId = product.ProductId,
                    ItemCount = product.ItemCount
                };
                _orderCommandHandler.Handle(orderCommand);
            }
        }
    }
}
