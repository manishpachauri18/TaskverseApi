using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskVerseApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {

        [HttpGet]
        protected string GetInfo()
        {
            return "Only Authorised By the Admin Policy";
        }
    }
}
