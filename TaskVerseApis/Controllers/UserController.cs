using Microsoft.AspNetCore.Mvc;
using TaskVerseApis.DTOS;
using TaskVerseApis.Services;
using TaskVerseApis.Models;

namespace TaskVerseApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // ===================== Register =====================
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // 1️⃣ Create user
                var user = await _userService.CreateUserAsync(model);
                if (user == null)
                    return BadRequest(new { message = "User creation failed. Email may already exist or invalid data." });

                // 2️⃣ Add profile picture if provided
                if (model.ProfilePicture != null)
                {
                    var result = await _userService.AddProfilePictureAsync(model, user.Id);
                    if (!result)
                    {
                        // Optional: You can either continue or rollback user creation if profile picture fails
                        return BadRequest(new { message = "User created but adding profile picture failed." });
                    }
                }

                return Ok(new
                {
                    message = "User registered successfully",
                    userId = user.Id,
                    email = user.Email,
                    fullName = user.FullName,
                    role = model.Role ?? "User",
                    profilePicture = user.ProfilePicturePath
                });
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error with the exception message
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }
    }
}
