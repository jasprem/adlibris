namespace Order.Events
{
    public class OrderFailedEvent
    {
        public string CartId { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
    }
}