using Microsoft.AspNetCore.Identity;
using TaskVerseApis.DTOS;
using TaskVerseApis.Models;

namespace TaskVerseApis.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<ApplicationUser?> GetByIdAsync(string userId);
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task<bool> CreateUserAsync(ApplicationUser user, string password);
        Task<bool> UpdateUserAsync(ApplicationUser user);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<bool> AddToRoleAsync(ApplicationUser user, string roleName);
        Task<bool> RemoveFromRoleAsync(ApplicationUser user, string roleName);
        Task<IdentityResult> AddUserAsync(ApplicationUser user, string password);
        Task<bool> AddUserToRoleAsync(ApplicationUser user, string roleName);
        Task<bool> AddProfilePictureAsync(RegisterDTO model, string userid);     
        Task<bool> UpdateProfilePictureAsync(RegisterDTO model,string userid);
    }
}
