namespace Catalog.Domain
{
    public class Order
    {
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public string Shelf { get; set; }
        public int NumberOfItems { get; set; }

        public Order(string customerId, string productId, string shelf, int numberOfItems)
        {
            CustomerId = customerId;
            ProductId = productId;
            Shelf = shelf;
            NumberOfItems = numberOfItems;
        }
    }
}
