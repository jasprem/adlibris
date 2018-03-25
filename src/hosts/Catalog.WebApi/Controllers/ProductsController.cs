using Catalog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly ProductStatusService _productStatusService;

        public ProductsController(
            ProductStatusService productStatusService)
        {
            _productStatusService = productStatusService;
        }

        [HttpGet("{productId}/status")]
        public IActionResult GetProductStatus(string productId)
        {
            var productStatus = _productStatusService.GetProductStatus(productId);
            if (productStatus == null)
            {
                return NotFound();
            }
            return Ok(productStatus);
        }
    }
}
