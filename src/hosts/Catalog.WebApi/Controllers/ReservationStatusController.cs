using Catalog.WebApi.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ReservationStatusController : Controller
    {
        public ReservationStatusController()
        {
            
        }

        [HttpGet("")]
        public IActionResult GetReservationStatus([FromBody] ReservationStatusParameter reservationStatusParameter)
        {
            return Ok();
        }
    }
}
