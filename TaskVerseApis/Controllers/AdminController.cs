using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskVerseApis.Controllers
{
    

        [Authorize]
        [Route("api/[controller]")]
        [ApiController]
        public class AdminController : ControllerBase
        {
            [HttpGet("GetInfo")]
            public IActionResult GetInfo()
            {
                return Ok("Only Authorised By the Admin Policy");
            }
        }

    
}
