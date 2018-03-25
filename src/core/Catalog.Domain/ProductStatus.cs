namespace Catalog.Domain
{
    public class ProductStatus
    {
        public string ProductId { get; set; }
        public int TotalAvailable { get; set; }

        public ProductStatus(string productId, int totalAvailable)
        {
            ProductId = productId;
            TotalAvailable = totalAvailable;
        }

        public void UpdateAvailable(int available)
        {
            TotalAvailable = available;
        }
    }
}