using Order.Events;

namespace Order.EventProcessors
{
    public class OrderEventHandler
    {
        private readonly OrderSucceededEventService _orderSucceededEventService;
        private readonly OrderFailedEventService _orderFailedEventService;

        public OrderEventHandler(
            OrderSucceededEventService orderSucceededEventService,
            OrderFailedEventService orderFailedEventService)
        {
            _orderSucceededEventService = orderSucceededEventService;
            _orderFailedEventService = orderFailedEventService;
        }

        public void Handle(OrderSucceededEvent orderSucceededEvent)
        {

        }

        public void Handle(OrderFailedEvent orderFailedEvent)
        {

        }
    }
}
    