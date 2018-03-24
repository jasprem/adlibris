using System;
using Catalog.Commands;

namespace Catalog.CommandProcessors
{
    public class OrderCommandHandler
    {
        private readonly OrderService _orderService;

        public OrderCommandHandler(OrderService orderService)
        {
            _orderService = orderService;
        }

        public void Handle(OrderCommand orderCommand)
        {
            _orderService.ExecuteOrder(orderCommand);
        }
    }
}
