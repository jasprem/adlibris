namespace Order.Events
{
    public class OrderSucceededEvent
    {
        public string CustomerId { get; set; }
        public string CartId { get; set; }
        public string ProductId { get; set; }
        public int TotalOrdered { get; set; }
    }
}
