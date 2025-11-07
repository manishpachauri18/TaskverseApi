using Microsoft.AspNetCore.Mvc;
using TaskVerseApis.DTOS;
using TaskVerseApis.Services;

namespace TaskVerseApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        // ===================== Login =====================
        [HttpPost("login")]
        public async Task<IActionResult> Login( LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tokens = await _userService.LoginAsync(model.Email, model.Password);
            if (tokens == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new
            {
                accessToken = tokens.Value.AccessToken,
                refreshToken = tokens.Value.RefreshToken
            });
        }
    }
}
//another comment okk