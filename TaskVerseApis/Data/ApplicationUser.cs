using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaskVerseApis.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Profile
        public string FullName { get; set; } = string.Empty;
        public string? ProfilePicturePath { get; set; }

        // NOTE: prefer roles via IdentityRole; keep RoleName as convenience only if needed
        public string? RoleName { get; set; }

        // Audit & status
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Project> CreatedProjects { get; set; } = new List<Project>();
        public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
        public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
