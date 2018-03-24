using System.Collections.Generic;

namespace Catalog.WebApi.Contracts
{
    public class CheckoutParameter
    {
        public string CustomerId { get; set; }
        public CartParameter CartParameter { get; set; }
    }

    public class CartParameter
    {
        public string CartId { get; set; }
        public List<ProductParameter> Products { get; set; }
    }

    public class ProductParameter
    {
        public string ProductId { get; set; }
        public int ItemCount { get; set; }
    }
}