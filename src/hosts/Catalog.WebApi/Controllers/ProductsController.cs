using Catalog.Application.Services;
using Catalog.WebApi.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ProductStatusService _productStatusService;

        public ProductsController(
            ProductStatusService productStatusService)
        {
            _productStatusService = productStatusService;
        }

        [HttpGet("{productId}/status")]
        public IActionResult GetProductAvailability(string productId)
        {
            var productStatus = _productStatusService.GetProductStatus(productId);
            return Ok(productStatus);
        }
    }
}
