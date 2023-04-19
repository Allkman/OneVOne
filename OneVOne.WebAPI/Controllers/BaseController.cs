using Microsoft.AspNetCore.Mvc;

namespace OneVOne.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult Error(string errorMessage)
        {
            return BadRequest(new { message = errorMessage });
        }

        protected IActionResult NotFound(string message = "Resource not found")
        {
            return NotFound(new { message });
        }

        protected IActionResult Success(object data = null)
        {
            return Ok(new { data });
        }
    }
}
