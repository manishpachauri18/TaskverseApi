using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskVerseApis.Models;

namespace TaskVerseApis.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // call Identity first

            // --- Project CreatedBy
            builder.Entity<Project>()
                .HasOne(p => p.CreatedBy)
                .WithMany(u => u.CreatedProjects)
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // --- ProjectUser join table (many-to-many)
            builder.Entity<ProjectUser>()
                .HasKey(pu => new { pu.ProjectId, pu.UserId });

            builder.Entity<ProjectUser>()
                .HasOne(pu => pu.Project)
                .WithMany(p => p.ProjectUsers)
                .HasForeignKey(pu => pu.ProjectId)
                .OnDelete(DeleteBehavior.Cascade); // safe: remove join rows when project removed

            builder.Entity<ProjectUser>()
                .HasOne(pu => pu.User)
                .WithMany(u => u.ProjectUsers)
                .HasForeignKey(pu => pu.UserId)
                .OnDelete(DeleteBehavior.Cascade); // safe: remove join rows when user removed

            // --- TaskItem -> Project
            builder.Entity<TaskItem>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Restrict); // prefer restrict with soft-delete

            // --- TaskItem -> AssignedTo
            builder.Entity<TaskItem>()
                .HasOne(t => t.AssignedTo)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedToId)
                .OnDelete(DeleteBehavior.SetNull); // preserve task when user removed

            // --- Comment -> TaskItem
            builder.Entity<Comment>()
                .HasOne(c => c.TaskItem)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TaskItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Comment -> CreatedBy
            builder.Entity<Comment>()
                .HasOne(c => c.CreatedBy)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // --- RefreshToken -> User
            builder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.Entity<RefreshToken>().HasIndex(rt => rt.Token).IsUnique(false);
            builder.Entity<RefreshToken>().HasIndex(rt => rt.UserId);
            builder.Entity<ProjectUser>().HasIndex(pu => pu.UserId);
            builder.Entity<TaskItem>().HasIndex(t => t.AssignedToId);
            builder.Entity<Project>().HasIndex(p => p.CreatedById);
            builder.Entity<Comment>().HasIndex(c => c.TaskItemId);

            // Global query filters for soft-delete (optional)
            builder.Entity<Project>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<TaskItem>().HasQueryFilter(t => !t.IsDeleted);
            builder.Entity<Comment>().HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
