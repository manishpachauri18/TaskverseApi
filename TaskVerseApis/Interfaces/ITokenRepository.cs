using TaskVerseApis.Models;

namespace TaskVerseApis.Interfaces
{
    public interface ITokenRepository
    {
        string GenerateAccessToken(ApplicationUser user, IList<string> roles);
        string GenerateRefreshToken();

        Task AddRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken?> GetRefreshTokenAsync(string token);
        Task<bool> RevokeRefreshTokenAsync(string token);
    }
}
