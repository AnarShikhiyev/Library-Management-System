using ProjectLibrary_Back.DTO;

namespace ProjectLibrary_Back.Interfaces
{
    public interface IUserAuthService
    {
        Task<bool> RegisterUserAsync(RegisterUserDto dto);
        Task<string?> LoginAsync(LoginDto dto);
        Task<bool> LogoutAsync(int userId);
    }
}
