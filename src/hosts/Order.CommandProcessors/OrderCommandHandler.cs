using Order.Commands;

namespace Order.CommandProcessors
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
