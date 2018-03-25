using Catalog.Domain;
using Catalog.Persistence;

namespace Catalog.Application.Services
{
    public class ProductStatusService
    {
        private readonly IProductRepository _productRepository;

        public ProductStatusService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductStatus GetProductStatus(string productId)
        {
            return _productRepository.GetProductStatus(productId);
        }
    }
}