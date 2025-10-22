using Microsoft.AspNetCore.Identity;
using TaskVerseApis.DTOS;
using TaskVerseApis.Interfaces;
using TaskVerseApis.Models;

namespace TaskVerseApis.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenRepository _tokenRepo;

        public UserService(IUserRepository userRepo, ITokenRepository tokenRepo)
        {
            _userRepo = userRepo;
            _tokenRepo = tokenRepo;
        }

        // ===================== Login =====================
        public async Task<(string AccessToken, string RefreshToken)?> LoginAsync(string email, string password)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user == null || !user.IsActive) return null;

            if (!await _userRepo.CheckPasswordAsync(user, password)) return null;

            var roles = await _userRepo.GetUserRolesAsync(user);
            var accessToken = _tokenRepo.GenerateAccessToken(user, roles);
            var refreshTokenValue = _tokenRepo.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                Token = refreshTokenValue,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _tokenRepo.AddRefreshTokenAsync(refreshToken);

            return (accessToken, refreshTokenValue);
        }

        // ===================== Revoke Token =====================
        public async Task<bool> RevokeTokenAsync(string token)
        {
            return await _tokenRepo.RevokeRefreshTokenAsync(token);
        }

        // ===================== Create User =====================
        public async Task<ApplicationUser?> CreateUserAsync(RegisterDTO model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                FullName = model.FullName,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userRepo.AddUserAsync(user, model.Password);
            if (!result.Succeeded) return null;

            if (!string.IsNullOrEmpty(model.Role))
            {
                await _userRepo.AddUserToRoleAsync(user, model.Role);
            }

            return user;
        }

        public async Task<bool> AddProfilePictureAsync(RegisterDTO model,string userId)
        {
            return await _userRepo.AddProfilePictureAsync(model, userId);
        }

        // ===================== Update Profile Picture =====================
        public async Task<bool> UpdateProfilePictureAsync(RegisterDTO model, string userId)
        {
            return await _userRepo.UpdateProfilePictureAsync(model, userId);
        }
    }
}
