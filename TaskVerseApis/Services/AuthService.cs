using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskVerseApis.Data;
using TaskVerseApis.DTOS;
using TaskVerseApis.Interfaces;
using TaskVerseApis.Models;


namespace TaskVerseAPIs.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenRepository _tokenService;
        private readonly IUserRepository _userRepository;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenRepository tokenService,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<TokenResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
            //var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            //if (user == null) return null;

            //var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            //if (!result.Succeeded) return null;

            //var roles = await _userManager.GetRolesAsync(user);
            //var accessToken = _tokenService.GenerateAccessToken(user, roles);
            //var refreshToken = _tokenService.GenerateRefreshToken();

            //user.RefreshToken = refreshToken;
            //user.RefreshTokenExpiryTime = System.DateTime.UtcNow.AddMinutes(60);
            //await _userRepository.UpdateAsync(user);

            //return new TokenResponseDTO
            //{
            //    AccessToken = accessToken,
            //    RefreshToken = refreshToken,
            //    AccessTokenExpiry = System.DateTime.UtcNow.AddMinutes(15),
            //    RefreshTokenExpiry = user.RefreshTokenExpiryTime.Value
            //};
        }

        public async Task<TokenResponseDTO> RefreshTokenAsync(RefreshRequestDTO refreshDTO)
        {
            throw new NotImplementedException();
            //var user = await _userRepository.GetByRefreshTokenAsync(refreshDTO.RefreshToken);
            //if (user == null || user.RefreshTokenExpiryTime < System.DateTime.UtcNow)
            //    return null;

            //var roles = await _userManager.GetRolesAsync(user);
            //var accessToken = _tokenService.GenerateAccessToken(user, roles);
            //var refreshToken = _tokenService.GenerateRefreshToken();

            //user.RefreshToken = refreshToken;
            //user.RefreshTokenExpiryTime = System.DateTime.UtcNow.AddMinutes(60);
            //await _userRepository.UpdateAsync(user);

            //return new TokenResponseDTO
            //{
            //    AccessToken = accessToken,
            //    RefreshToken = refreshToken,
            //    AccessTokenExpiry = System.DateTime.UtcNow.AddMinutes(15),
            //    RefreshTokenExpiry = user.RefreshTokenExpiryTime.Value
            //};
        }

        public async Task<bool> RegisterAsync(RegisterDTO registerDTO, string currentUserRole)
        {
            var user = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                FullName = registerDTO.FullName,
                IsActive = true
            };

            var role = UserRole.User.ToString();
            if (currentUserRole == UserRole.Admin.ToString() && !string.IsNullOrEmpty(registerDTO.Role))
            {
                role = registerDTO.Role;
            }

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded) return false;

            await _userManager.AddToRoleAsync(user, role);
            // TODO: Save Profile Picture if registerDTO.ProfilePicture != null

            return true;
        }

        public async Task LogoutAsync(string userId)
        {
            throw new NotImplementedException();
            //var user = await _userRepository.GetByIdAsync(userId);
            //if (user == null) return;

            //user.RefreshToken = null;
            //user.RefreshTokenExpiryTime = null;
            //await _userRepository.UpdateAsync(user);
        }
    }
}
