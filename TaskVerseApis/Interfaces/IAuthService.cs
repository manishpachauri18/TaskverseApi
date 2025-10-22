using TaskVerseApis.DTOS;

namespace TaskVerseApis.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDTO> LoginAsync(LoginDTO loginDTO);
        Task<TokenResponseDTO> RefreshTokenAsync(RefreshRequestDTO refreshDTO);
        Task<bool> RegisterAsync(RegisterDTO registerDTO, string currentUserRole);
        Task LogoutAsync(string userId);
    }
}
