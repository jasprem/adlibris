using Catalog.Commands;
using Catalog.Domain;
using Catalog.Persistence;

namespace Catalog.CommandProcessors
{
    public class UpdateProductStatusService
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductStatusService(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void UpdateProductStatus(UpdateProductStatusCommand command)
        {
            var productStatus = _productRepository.GetProductStatus(command.ProductId);
            if (productStatus == null)
            {
                _productRepository.AddNewProductStatus(new ProductStatus(command.ProductId, command.TotalAfterOrderExecuted));
            }
            else
            {
                productStatus.UpdateAvailable(command.TotalAfterOrderExecuted);
                _productRepository.UpdateProductStatus(productStatus);
            }
        }
    }
}