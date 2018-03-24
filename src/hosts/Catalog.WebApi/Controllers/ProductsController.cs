using Catalog.WebApi.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        public ProductsController()
        {
            
        }

        [HttpGet("{productId}")]
        public IActionResult GetProduct(string productId)
        {
            return Ok();
        }

        [HttpGet("{productId}/status")]
        public IActionResult GetProductAvailability(string productId)
        {
            var productAvailablity = new ProductStatusParameter();
            return Ok(productAvailablity);
        }
    }
}
