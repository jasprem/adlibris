using System.Collections.Generic;
using System.Linq;

namespace Catalog.Domain
{
    public class ProductAggregate
    {
        public IEnumerable<Product> Products { get; }

        public ProductAggregate(IEnumerable<Product> products)
        {
            Products = products;
        }

        public int GetTotalAvailable()
        {
            return Products.Sum(product => product.Available);
        }
    }

    public class Product
    {
        public string ProductId { get; set; }
        public string Shelf { get; set; }
        public int Available { get; set; }

        public void Remove(int numberOfItems)
        {
            Available = Available - numberOfItems;
        }
    }
}