using Microsoft.EntityFrameworkCore;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Interfaces;
using ProjectLibrary_Back.Models;

namespace ProjectLibrary_Back.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryProjectContext _context;

        public UserService(LibraryProjectContext context)
        {
            _context = context;
        }
        public async Task<UserDto> AddUserAsync(AddUserDto newUser)
        {
            var user = new User
            {
                Firstname = newUser.Firstname,
                Lastname = newUser.Lastname,
                Username = newUser.Username,
                Password = newUser.Password,
                RoleId = newUser.RoleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                RoleId = user.RoleId
            };
        }

        public async Task<bool> DeactivateUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

   
            user.Active = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users.Select(user => new UserDto
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                RoleId = user.RoleId,
                Active = user.Active
            }).ToListAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                RoleId = user.RoleId,
                Active= user.Active
            };
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                RoleId = user.RoleId,
                Active= user.Active
            };
        }

        public async Task<List<UserDto>> GetUsersByRoleAsync(int roleId)
        {
            return await _context.Users.Where(u => u.RoleId == roleId)
                                   .Select(user => new UserDto
                                   {
                                       Id = user.Id,
                                       Firstname = user.Firstname,
                                       Lastname = user.Lastname,
                                       Username = user.Username,
                                       RoleId = user.RoleId
                                   }).ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            
            if (updatedUser.RoleId.HasValue && !await _context.Roles.AnyAsync(r => r.Id == updatedUser.RoleId.Value))
                return false;

            user.Firstname = updatedUser.Firstname;
            user.Lastname = updatedUser.Lastname;
            user.Username = updatedUser.Username;
            user.Password = updatedUser.Password; 
            user.RoleId = updatedUser.RoleId;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
