using Catalog.Commands;

namespace Catalog.CommandProcessors
{
    public class UpdateProductStatusCommandHandler
    {
        private readonly UpdateProductStatusService _productStatusService;

        public UpdateProductStatusCommandHandler(
            UpdateProductStatusService productStatusService)
        {
            _productStatusService = productStatusService;
        }

        public void Handle(UpdateProductStatusCommand command)
        {
            _productStatusService.UpdateProductStatus(command);
        }
    }
}