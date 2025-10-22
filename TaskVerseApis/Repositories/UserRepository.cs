using Microsoft.AspNetCore.Identity;
using TaskVerseApis.DTOS;
using TaskVerseApis.Interfaces;
using TaskVerseApis.Models;

namespace TaskVerseApis.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public UserRepository(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        //new line commentsafdsfdsfsdfsfsdfsdfdsfdsfds
        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser?> GetByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            if (user == null) return false;

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
            return result.Succeeded;
        }
        public async Task<bool> UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        // ===================== NEW: Add User =====================
        public async Task<IdentityResult> AddUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<bool> AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole(roleName));

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public Task<bool> CreateUserAsync(ApplicationUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddToRoleAsync(ApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddProfilePictureAsync(RegisterDTO model, string userId)
        {
            if (model.ProfilePicture == null || model.ProfilePicture.Length == 0)
                return false;

            var user = await _userManager.FindByIdAsync(userId);
            if (userId == null) return false;

            var folderName = string.IsNullOrEmpty(model.Role) ? "users" : model.Role.ToLower() switch
            {
                "admin" => "admins",
                "manager" => "managers",
                _ => "users"
            };

            var appRoot = Directory.GetCurrentDirectory();
            var folderPath = Path.Combine(appRoot, "resources", "profilepictures", folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{user.Id}_{Path.GetFileName(model.ProfilePicture.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await model.ProfilePicture.CopyToAsync(stream);

            user.ProfilePicturePath = Path.Combine("resources", "profilepictures", folderName, fileName).Replace("\\", "/");
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> UpdateProfilePictureAsync(RegisterDTO model, string userId)
        {
            if (model.ProfilePicture == null || model.ProfilePicture.Length == 0)
                return false;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Delete old picture if exists
            if (!string.IsNullOrEmpty(user.ProfilePicturePath))
            {
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), user.ProfilePicturePath);
                if (File.Exists(oldPath))
                    File.Delete(oldPath);
            }

            var folderName = string.IsNullOrEmpty(model.Role) ? "users" : model.Role.ToLower() switch
            {
                "admin" => "admins",
                "manager" => "managers",
                _ => "users"
            };

            var appRoot = Directory.GetCurrentDirectory();
            var folderPath = Path.Combine(appRoot, "resources", "profilepictures", folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{user.Id}_{Path.GetFileName(model.ProfilePicture.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await model.ProfilePicture.CopyToAsync(stream);

            user.ProfilePicturePath = Path.Combine("resources", "profilepictures", folderName, fileName).Replace("\\", "/");
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

    }
}
