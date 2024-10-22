using ProjectLibrary_Back.DTO;

namespace ProjectLibrary_Back.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> AddUserAsync(AddUserDto newUser);
        Task<bool> UpdateUserAsync(int id, UpdateUserDto updatedUser);
        Task<bool> DeleteUserAsync(int id);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<List<UserDto>> GetUsersByRoleAsync(int roleId);
        Task<bool> DeactivateUserAsync(int id);
    }
}
