using Catalog.Application.Services;
using Catalog.WebApi.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CheckoutController : Controller
    {
        private readonly CheckoutService _checkoutService;

        public CheckoutController(
            CheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost("")]
        public IActionResult CheckoutProduct([FromBody] CheckoutParameter checkoutParameter)
        {
            _checkoutService.Checkout(checkoutParameter);
            return Ok();
        }
    }
}
