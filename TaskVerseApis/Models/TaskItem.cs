using System.ComponentModel.DataAnnotations;

namespace TaskVerseApis.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public string? AssignedToId { get; set; }
        public ApplicationUser? AssignedTo { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedById { get; set; }

        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = "ToDo";

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        [Timestamp] public byte[]? RowVersion { get; set; }
    }

}