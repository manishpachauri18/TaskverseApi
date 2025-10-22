namespace TaskVerseApis.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }

        // Token string should be stored securely (hashed if you want more security)
        public string Token { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public string DeviceInfo { get; set; } = string.Empty; // e.g. "web-chrome-109", optional
        public string IpAddress { get; set; } = string.Empty; // optional logging

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; } // when token was revoked
        public string? ReplacedByToken { get; set; } // token rotation chain
        public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpiresAt;
    }
}
