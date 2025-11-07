using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskVerseApis.Controllers
{

    [Authorize(Policy = "RequireAdminRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet("GetInfo")]
        public IActionResult GetInfo()
        {
            return Ok("Only authorised by the Admin policy");
        }
    }



}
