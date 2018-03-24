namespace Catalog.Domain
{
    public class Order
    {
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public string Shelf { get; set; }
        public int TotalOrdered { get; set; }

        public Order(string customerId, string productId, string shelf, int totalOrdered)
        {
            CustomerId = customerId;
            ProductId = productId;
            Shelf = shelf;
            TotalOrdered = totalOrdered;
        }
    }
}
